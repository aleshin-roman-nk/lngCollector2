# Примеры частичного обновления в Entity Framework Core

## 1. Optional Pattern (рекомендуемый подход)

### Описание
Использует структуру `Optional<T>` для четкого разделения между "не передано" и "передано null".

### Пример DTO
```csharp
public class PatchFlashCardDto : PatchDtoBase, IPatchable<FlashCard, PatchFlashCardDto>
{
    public Optional<string?> Question { get; set; }
    public Optional<string?> Description { get; set; }
    public Optional<int> LanguageId { get; set; }
    // ... другие поля
}
```

### Пример использования (HTTP PATCH)
```http
PATCH /api/flashcard/patch
Content-Type: application/json

{
  "id": 123,
  "question": "Обновленный вопрос",
  "languageId": 2
}
```

**Результат**: Обновятся только `question` и `languageId`, остальные поля останутся без изменений.

### Преимущества
- ✅ Типобезопасность
- ✅ Четкая семантика (HasValue/Value)
- ✅ Поддержка валидации
- ✅ IntelliSense в IDE

### Недостатки
- ❌ Более сложная структура
- ❌ Больше кода для написания

## 2. JSON Patch RFC 6902

### Описание
Стандартизированный подход для частичного обновления согласно RFC 6902.

### Пример использования (HTTP PATCH)
```http
PATCH /api/flashcard/jsonpatch
Content-Type: application/json

{
  "id": 123,
  "operations": [
    { "op": "replace", "path": "/question", "value": "Новый вопрос" },
    { "op": "replace", "path": "/hitsInRow", "value": 5 },
    { "op": "remove", "path": "/description" }
  ]
}
```

### Поддерживаемые операции
- `replace` - заменить значение
- `add` - добавить значение
- `remove` - удалить значение (установить в default)
- `test` - проверить текущее значение

### Преимущества
- ✅ Стандартизированный подход
- ✅ Поддержка сложных операций
- ✅ Атомарность операций
- ✅ Возможность валидации (test operation)

### Недостатки
- ❌ Сложность для простых случаев
- ❌ Менее читаемый JSON

## 3. Простой динамический подход

### Описание
Использует анонимные объекты и рефлексию для простых случаев.

### Пример использования (HTTP PATCH)
```http
PATCH /api/flashcard/123/simple
Content-Type: application/json

{
  "question": "Обновленный вопрос",
  "hitsInRow": 3
}
```

### Преимущества
- ✅ Простота использования
- ✅ Минимум кода
- ✅ Гибкость

### Недостатки
- ❌ Отсутствие типобезопасности
- ❌ Нет валидации на уровне компиляции
- ❌ Использование рефлексии (производительность)

## 4. Сравнение подходов

| Критерий | Optional Pattern | JSON Patch | Dynamic |
|----------|------------------|------------|---------|
| Типобезопасность | ✅ Высокая | ⚠️ Средняя | ❌ Низкая |
| Производительность | ✅ Высокая | ✅ Высокая | ⚠️ Средняя |
| Простота использования | ⚠️ Средняя | ❌ Сложная | ✅ Высокая |
| Валидация | ✅ Полная | ⚠️ Частичная | ❌ Минимальная |
| Стандартизация | ⚠️ Custom | ✅ RFC 6902 | ❌ Custom |
| Поддержка IDE | ✅ Полная | ⚠️ Частичная | ❌ Минимальная |

## 5. Рекомендации по выбору подхода

### Выбирайте Optional Pattern когда:
- Нужна высокая типобезопасность
- Важна производительность
- Требуется полная валидация
- Команда работает в строго типизированном стиле

### Выбирайте JSON Patch когда:
- Нужна стандартизация
- Требуются сложные операции обновления
- Интеграция с внешними системами
- Нужна поддержка транзакционности

### Выбирайте Dynamic подход когда:
- Прототипирование или MVP
- Простые случаи обновления
- Нужна максимальная гибкость
- Ограниченное время разработки

## 6. Лучшие практики

### 1. Всегда используйте валидацию
```csharp
[StringLength(1000, ErrorMessage = "Question cannot exceed 1000 characters")]
public Optional<string?> Question { get; set; }
```

### 2. Обрабатывайте исключения
```csharp
try
{
    var result = await patchService.PatchAsync<FlashCard, PatchFlashCardDto>(patchDto);
    return Ok(result);
}
catch (ValidationException ex)
{
    return BadRequest(new { error = ex.Message });
}
```

### 3. Используйте транзакции для сложных обновлений
```csharp
using var transaction = await _context.Database.BeginTransactionAsync();
try
{
    // Множественные обновления
    await transaction.CommitAsync();
}
catch
{
    await transaction.RollbackAsync();
    throw;
}
```

### 4. Логируйте изменения для аудита
```csharp
public async Task<TEntity> PatchAsync<TEntity, TPatchDto>(TPatchDto patchDto) 
{
    _logger.LogInformation("Patching {EntityType} with ID {Id}", typeof(TEntity).Name, patchDto.Id);
    // ... логика обновления
}
```

### 5. Используйте Change Tracking EF Core
```csharp
var entity = await _context.FlashCards.FindAsync(id);
// EF автоматически отслеживает изменения
entity.Question = newQuestion;
await _context.SaveChangesAsync(); // Обновит только измененные поля
```