# Техническое задание: Органайзер изучения английского языка

## 1. Общее описание проекта

### 1.1 Название проекта
**EnglishMaster** - веб-приложение для организации и отслеживания прогресса изучения английского языка

### 1.2 Цель проекта
Создать интерактивную платформу для изучения английского языка с элементами геймификации, которая поможет пользователям поддерживать ежедневную мотивацию и отслеживать прогресс обучения.

### 1.3 Целевая аудитория
- Изучающие английский язык (уровни A1-C2)
- Возраст: 16-65 лет
- Пользователи, предпочитающие структурированное обучение
- Люди, желающие отслеживать свой прогресс

## 2. Функциональные требования

### 2.1 Система аутентификации
- Регистрация пользователей (email + password)
- Авторизация через JWT токены
- Восстановление пароля
- Профиль пользователя с настройками

### 2.2 Модульная система обучения

#### 2.2.1 Структура модуля
- **Модуль** - тематический блок обучения
- **Источник материала**: текст, аудио, видео, изображения
- **Набор заданий** различных типов
- **Прогресс выполнения** (визуализация в виде сетки клеток)

#### 2.2.2 Типы источников материала
- Текстовые материалы (рассказы, статьи)
- Аудиофайлы (песни, подкасты, диалоги)
- Видеоматериалы (фильмы, обучающие ролики)
- Изображения с описаниями

### 2.3 Типы заданий

#### 2.3.1 Карточки повторения (Flashcards)
**Входные данные:**
- Вопрос (слово, фраза, изображение)
- Правильные варианты ответов

**Функционал:**
- Ввод ответа пользователем
- Проверка корректности
- Система интервального повторения (Spaced Repetition)

#### 2.3.2 Перевод предложений
**Входные данные:**
- Предложение на русском/английском
- Эталонные варианты перевода

**Функционал:**
- Ввод перевода пользователем
- Двухэтапная проверка:
  1. Поиск в базе эталонных ответов
  2. Проверка через AI API при отсутствии точного совпадения
- Кеширование AI-ответов в БД

#### 2.3.3 Аудио диктант
**Входные данные:**
- Аудиофайл
- Эталонный текст

**Функционал:**
- Воспроизведение аудио
- Ввод распознанного текста
- Сравнение с эталоном (точность ≥90%)
- Подсветка ошибок

### 2.4 Система прогресса и геймификации

#### 2.4.1 Визуализация модулей
- Модуль представлен как прямоугольная сетка
- Каждая клетка = одно задание
- Цветовая индикация статуса:
  - Серый: не выполнено
  - Желтый: в процессе
  - Зеленый: выполнено успешно
  - Красный: выполнено с ошибками

#### 2.4.2 Система достижений
- Ежедневные цели (streak)
- Недельные/месячные статистики
- Бейджи за достижения
- Рейтинговая система

#### 2.4.3 Аналитика прогресса
- Общая статистика обучения
- Графики прогресса по времени
- Анализ слабых мест
- Рекомендации по улучшению

## 3. Техническая архитектура

### 3.1 Технологический стек

#### Backend
- **Framework**: ASP.NET Core 8
- **Database**: PostgreSQL
- **ORM**: Entity Framework Core
- **Authentication**: JWT Bearer tokens
- **API Documentation**: Swagger/OpenAPI
- **Caching**: Redis
- **File Storage**: Local storage / Azure Blob Storage
- **Background Jobs**: Hangfire

#### Frontend
- **Framework**: Angular 17+
- **State Management**: NgRx
- **UI Components**: Angular Material
- **Charts**: Chart.js / D3.js
- **Audio**: Web Audio API
- **PWA**: Service Workers

#### External Services
- **AI API**: OpenAI GPT-4 / Claude API
- **Audio Processing**: Web Speech API
- **Email**: SendGrid / SMTP

### 3.2 Архитектурные принципы
- Clean Architecture
- Repository Pattern
- CQRS (Command Query Responsibility Segregation)
- Dependency Injection
- Unit of Work Pattern

### 3.3 Схема базы данных

```sql
-- Пользователи
Users
- Id (PK)
- Email
- PasswordHash
- FirstName
- LastName
- RegistrationDate
- LastLoginDate
- IsActive

-- Модули обучения
Modules
- Id (PK)
- UserId (FK)
- Title
- Description
- Category
- DifficultyLevel
- CreatedDate
- IsCompleted
- TotalTasks
- CompletedTasks

-- Источники материалов
ModuleSources
- Id (PK)
- ModuleId (FK)
- SourceType (Text/Audio/Video/Image)
- Title
- Content
- FilePath
- Duration

-- Задания
Tasks
- Id (PK)
- ModuleId (FK)
- TaskType (Flashcard/Translation/Dictation)
- Question
- CorrectAnswers (JSON)
- Difficulty
- Points
- Position (для сетки)

-- Ответы пользователей
UserAnswers
- Id (PK)
- TaskId (FK)
- UserId (FK)
- Answer
- IsCorrect
- Score
- AttemptDate
- TimeSpent

-- Кеш AI-ответов
AiAnswerCache
- Id (PK)
- TaskId (FK)
- UserAnswer
- AiEvaluation
- Score
- IsApproved
- CachedDate

-- Прогресс пользователя
UserProgress
- Id (PK)
- UserId (FK)
- Date
- TasksCompleted
- TimeSpent
- StreakDays
- TotalPoints
```

## 4. API Endpoints

### 4.1 Authentication
```
POST /api/auth/register
POST /api/auth/login
POST /api/auth/refresh
POST /api/auth/forgot-password
POST /api/auth/reset-password
```

### 4.2 Users
```
GET /api/users/profile
PUT /api/users/profile
GET /api/users/statistics
```

### 4.3 Modules
```
GET /api/modules
POST /api/modules
GET /api/modules/{id}
PUT /api/modules/{id}
DELETE /api/modules/{id}
GET /api/modules/{id}/progress
```

### 4.4 Tasks
```
GET /api/modules/{moduleId}/tasks
POST /api/modules/{moduleId}/tasks
GET /api/tasks/{id}
PUT /api/tasks/{id}
DELETE /api/tasks/{id}
POST /api/tasks/{id}/answer
GET /api/tasks/{id}/answers
```

### 4.5 AI Integration
```
POST /api/ai/evaluate-translation
POST /api/ai/evaluate-answer
```

### 4.6 Progress & Analytics
```
GET /api/progress/dashboard
GET /api/progress/statistics
GET /api/progress/achievements
```

## 5. Пользовательский интерфейс

### 5.1 Главные страницы

#### Дашборд
- Сводка прогресса
- Активные модули
- Ежедневные цели
- Последние достижения

#### Библиотека модулей
- Список всех модулей
- Фильтры по категориям/сложности
- Поиск модулей
- Создание нового модуля

#### Страница модуля
- Информация о модуле
- Сетка заданий (progress grid)
- Кнопка начала/продолжения

#### Страница задания
- Интерфейс выполнения задания
- Таймер
- Подсказки
- Проверка ответа

#### Аналитика
- Графики прогресса
- Статистика по времени
- Анализ ошибок
- Рекомендации

### 5.2 Компоненты UI

#### ModuleGrid Component
```typescript
@Component({
  selector: 'app-module-grid',
  template: `
    <div class="module-grid">
      <div class="module-cell" 
           *ngFor="let task of tasks; trackBy: trackByFn"
           [class.completed]="task.isCompleted"
           [class.current]="task.isCurrent"
           (click)="onTaskClick(task)">
        {{ task.position }}
      </div>
    </div>
  `
})
```

#### TaskPlayer Component
```typescript
@Component({
  selector: 'app-task-player',
  template: `
    <div class="task-container">
      <app-audio-player *ngIf="task.type === 'dictation'"
                        [audioUrl]="task.audioUrl">
      </app-audio-player>
      
      <app-flashcard *ngIf="task.type === 'flashcard'"
                     [question]="task.question"
                     (answerSubmitted)="onAnswer($event)">
      </app-flashcard>
      
      <app-translation *ngIf="task.type === 'translation'"
                       [sentence]="task.sentence"
                       (translationSubmitted)="onTranslation($event)">
      </app-translation>
    </div>
  `
})
```

## 6. MVP Фазы разработки

### Фаза 1 (2 месяца) - Базовый функционал
- [ ] Система аутентификации
- [ ] CRUD для модулей
- [ ] Базовые типы заданий (flashcards, простой перевод)
- [ ] Простая визуализация прогресса

### Фаза 2 (2 месяца) - AI интеграция
- [ ] Интеграция с AI API
- [ ] Двухэтапная проверка ответов
- [ ] Кеширование AI-ответов
- [ ] Улучшенная система оценки

### Фаза 3 (2 месяца) - Аудио и геймификация
- [ ] Аудио диктанты
- [ ] Система достижений
- [ ] Детальная аналитика
- [ ] PWA функции

### Фаза 4 (2 месяца) - Полировка и оптимизация
- [ ] Мобильная адаптация
- [ ] Производительность
- [ ] Тестирование
- [ ] Деплой в продакшн

## 7. Нефункциональные требования

### 7.1 Производительность
- Время загрузки страницы: < 3 сек
- Время ответа API: < 500мс
- Поддержка 1000+ одновременных пользователей

### 7.2 Безопасность
- HTTPS для всех соединений
- Валидация входных данных
- Rate limiting для API
- Защита от SQL-инъекций и XSS

### 7.3 Совместимость
- Поддержка браузеров: Chrome 90+, Firefox 88+, Safari 14+
- Мобильная адаптация (responsive design)
- PWA для оффлайн использования

### 7.4 Масштабируемость
- Горизонтальное масштабирование
- Кеширование статического контента
- Оптимизация запросов к БД

## 8. Развертывание и DevOps

### 8.1 Инфраструктура
- **Backend**: Docker containers
- **Database**: PostgreSQL в Docker
- **Redis**: Кеширование
- **Web Server**: Nginx
- **Hosting**: Azure/AWS/DigitalOcean

### 8.2 CI/CD Pipeline
- GitHub Actions
- Автоматические тесты
- Деплой в staging/production
- Мониторинг и логирование

## 9. Тестирование

### 9.1 Backend
- Unit tests (xUnit)
- Integration tests
- API tests (Postman/Newman)

### 9.2 Frontend
- Unit tests (Jasmine/Karma)
- E2E tests (Cypress)
- Component tests

## 10. Документация

### 10.1 Техническая документация
- API документация (Swagger)
- Диаграммы архитектуры
- Инструкции по развертыванию

### 10.2 Пользовательская документация
- Руководство пользователя
- FAQ
- Видеоуроки

---

**Автор ТЗ**: AI Assistant  
**Дата создания**: 2024  
**Версия**: 1.0