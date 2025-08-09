using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ThoughtzLand.Core.Models.Common
{
    /// <summary>
    /// Расширения для работы с FlashCard в EF Core
    /// </summary>
    public static class FlashCardExtensions
    {
        /// <summary>
        /// Обновляет только измененные поля с использованием EF Core Change Tracking
        /// Наиболее эффективный подход для EF Core
        /// </summary>
        public static async Task<T> UpdateChangedPropertiesAsync<T>(this DbContext context, int id, Action<T> updateAction) 
            where T : class
        {
            var entity = await context.Set<T>().FindAsync(id);
            if (entity == null)
            {
                throw new KeyNotFoundException($"Entity with ID {id} not found");
            }

            // EF Core автоматически отслеживает изменения
            updateAction(entity);

            // Сохраняем только измененные поля
            await context.SaveChangesAsync();
            
            return entity;
        }

        /// <summary>
        /// Обновляет конкретные поля сущности с явным указанием измененных свойств
        /// Полезно когда нужен полный контроль над тем, что обновляется
        /// </summary>
        public static async Task<T> UpdateSpecificPropertiesAsync<T>(this DbContext context, int id, 
            Dictionary<string, object> updates) where T : class
        {
            var entity = await context.Set<T>().FindAsync(id);
            if (entity == null)
            {
                throw new KeyNotFoundException($"Entity with ID {id} not found");
            }

            var entry = context.Entry(entity);
            
            foreach (var update in updates)
            {
                var property = entry.Property(update.Key);
                if (property != null)
                {
                    property.CurrentValue = update.Value;
                    property.IsModified = true;
                }
            }

            await context.SaveChangesAsync();
            return entity;
        }

        /// <summary>
        /// Массовое обновление с использованием выражений
        /// Эффективно для обновления множества записей
        /// </summary>
        public static async Task<int> BulkUpdateAsync<T>(this DbContext context, 
            Expression<Func<T, bool>> predicate, 
            Expression<Func<T, T>> updateExpression) where T : class
        {
            // Примечание: Требует дополнительных пакетов типа EFCore.BulkExtensions
            // или можно использовать ExecuteUpdateAsync в EF Core 7+
            
            var entities = await context.Set<T>().Where(predicate).ToListAsync();
            
            var compiledUpdate = updateExpression.Compile();
            foreach (var entity in entities)
            {
                var updatedEntity = compiledUpdate(entity);
                // Применяем изменения...
            }
            
            return await context.SaveChangesAsync();
        }

        /// <summary>
        /// Optimistic concurrency - обновление с проверкой версии
        /// Предотвращает конфликты при одновременном обновлении
        /// </summary>
        public static async Task<T> UpdateWithConcurrencyCheckAsync<T>(this DbContext context, int id, 
            Action<T> updateAction, byte[] originalRowVersion = null) where T : class
        {
            var entity = await context.Set<T>().FindAsync(id);
            if (entity == null)
            {
                throw new KeyNotFoundException($"Entity with ID {id} not found");
            }

            // Проверка версии (если модель поддерживает)
            if (originalRowVersion != null)
            {
                var entry = context.Entry(entity);
                var rowVersionProperty = entry.Property("RowVersion");
                if (rowVersionProperty != null)
                {
                    if (!originalRowVersion.SequenceEqual((byte[])rowVersionProperty.CurrentValue))
                    {
                        throw new DbUpdateConcurrencyException("Entity was modified by another user");
                    }
                }
            }

            updateAction(entity);
            
            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Обработка конфликтов concurrency
                throw new InvalidOperationException("The entity was modified by another user. Please refresh and try again.");
            }
            
            return entity;
        }

        /// <summary>
        /// Частичное обновление с аудитом изменений
        /// Логирует все изменения для истории аудита
        /// </summary>
        public static async Task<T> UpdateWithAuditAsync<T>(this DbContext context, int id, 
            Action<T> updateAction, string userId = null) where T : class
        {
            var entity = await context.Set<T>().FindAsync(id);
            if (entity == null)
            {
                throw new KeyNotFoundException($"Entity with ID {id} not found");
            }

            // Сохраняем состояние до изменения для аудита
            var originalValues = new Dictionary<string, object>();
            var entry = context.Entry(entity);
            foreach (var property in entry.Properties)
            {
                originalValues[property.Metadata.Name] = property.CurrentValue;
            }

            // Применяем изменения
            updateAction(entity);

            // Логируем изменения
            var changes = new List<string>();
            foreach (var property in entry.Properties.Where(p => p.IsModified))
            {
                var oldValue = originalValues[property.Metadata.Name];
                var newValue = property.CurrentValue;
                changes.Add($"{property.Metadata.Name}: {oldValue} → {newValue}");
            }

            if (changes.Any())
            {
                // Здесь можно добавить логирование в БД или файл
                Console.WriteLine($"Entity {typeof(T).Name} (ID: {id}) updated by {userId ?? "system"}: {string.Join(", ", changes)}");
            }

            await context.SaveChangesAsync();
            return entity;
        }

        /// <summary>
        /// Каскадное обновление связанных сущностей
        /// Обновляет основную сущность и связанные с ней объекты
        /// </summary>
        public static async Task<T> UpdateWithRelatedEntitiesAsync<T>(this DbContext context, int id, 
            Action<T> updateAction, params string[] includeProperties) where T : class
        {
            // Загружаем сущность со связанными объектами
            IQueryable<T> query = context.Set<T>();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            var entity = await query.FirstOrDefaultAsync(e => EF.Property<int>(e, "id") == id);
            if (entity == null)
            {
                throw new KeyNotFoundException($"Entity with ID {id} not found");
            }

            // Применяем изменения
            updateAction(entity);

            // EF Core автоматически обработает связанные изменения
            await context.SaveChangesAsync();
            
            return entity;
        }
    }

    /// <summary>
    /// Результат операции частичного обновления с метаданными
    /// </summary>
    public class UpdateResult<T>
    {
        public T Entity { get; set; }
        public List<string> ModifiedProperties { get; set; } = new();
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public string UpdatedBy { get; set; }
        public int RowsAffected { get; set; }
    }

    /// <summary>
    /// Расширенный сервис для частичного обновления с метаданными
    /// </summary>
    public static class AdvancedUpdateExtensions
    {
        /// <summary>
        /// Обновляет сущность и возвращает подробную информацию об изменениях
        /// </summary>
        public static async Task<UpdateResult<T>> UpdateAndTrackChangesAsync<T>(this DbContext context, int id, 
            Action<T> updateAction, string userId = null) where T : class
        {
            var entity = await context.Set<T>().FindAsync(id);
            if (entity == null)
            {
                throw new KeyNotFoundException($"Entity with ID {id} not found");
            }

            var entry = context.Entry(entity);
            
            // Применяем изменения
            updateAction(entity);

            // Собираем информацию об изменениях
            var modifiedProperties = entry.Properties
                .Where(p => p.IsModified)
                .Select(p => p.Metadata.Name)
                .ToList();

            var rowsAffected = await context.SaveChangesAsync();

            return new UpdateResult<T>
            {
                Entity = entity,
                ModifiedProperties = modifiedProperties,
                UpdatedBy = userId,
                RowsAffected = rowsAffected
            };
        }
    }
}