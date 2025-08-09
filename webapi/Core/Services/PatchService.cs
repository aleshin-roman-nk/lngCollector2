using Microsoft.EntityFrameworkCore;
using ThoughtzLand.Core.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace ThoughtzLand.Core.Services
{
    /// <summary>
    /// Сервис для частичного обновления сущностей
    /// </summary>
    public class PatchService : IPatchService
    {
        private readonly DbContext _context;

        public PatchService(DbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Частично обновляет сущность
        /// </summary>
        public async Task<TEntity> PatchAsync<TEntity, TPatchDto>(TPatchDto patchDto) 
            where TEntity : class
            where TPatchDto : PatchDtoBase, IPatchable<TEntity, TPatchDto>, new()
        {
            return await PatchAsync<TEntity, TPatchDto>(patchDto, null);
        }

        /// <summary>
        /// Частично обновляет сущность с валидацией
        /// </summary>
        public async Task<TEntity> PatchAsync<TEntity, TPatchDto>(TPatchDto patchDto, Func<TEntity, Task<bool>> validator = null) 
            where TEntity : class
            where TPatchDto : PatchDtoBase, IPatchable<TEntity, TPatchDto>, new()
        {
            // Валидация DTO
            ValidateDto(patchDto);

            // Поиск сущности
            var entity = await _context.Set<TEntity>().FindAsync(patchDto.Id);
            if (entity == null)
            {
                throw new KeyNotFoundException($"Entity of type {typeof(TEntity).Name} with ID {patchDto.Id} not found");
            }

            // Применение изменений
            var patchInstance = new TPatchDto();
            patchInstance.ApplyPatch(entity, patchDto);

            // Дополнительная валидация (если предоставлена)
            if (validator != null)
            {
                var isValid = await validator(entity);
                if (!isValid)
                {
                    throw new ValidationException("Entity validation failed after applying patch");
                }
            }

            // Сохранение изменений
            await _context.SaveChangesAsync();

            return entity;
        }

        /// <summary>
        /// Валидирует DTO используя Data Annotations
        /// </summary>
        private void ValidateDto<TPatchDto>(TPatchDto dto) where TPatchDto : PatchDtoBase
        {
            var validationContext = new ValidationContext(dto);
            var validationResults = new List<ValidationResult>();

            if (!Validator.TryValidateObject(dto, validationContext, validationResults, true))
            {
                var errors = validationResults.Select(vr => vr.ErrorMessage);
                throw new ValidationException($"Validation failed: {string.Join("; ", errors)}");
            }
        }
    }

    /// <summary>
    /// Расширения для упрощения работы с частичными обновлениями
    /// </summary>
    public static class PatchExtensions
    {
        /// <summary>
        /// Применяет частичное обновление с использованием рефлексии
        /// Альтернативный подход для простых случаев
        /// </summary>
        public static void ApplyPatchByReflection<T>(this T entity, object patchDto) where T : class
        {
            var entityType = typeof(T);
            var patchType = patchDto.GetType();

            foreach (var patchProperty in patchType.GetProperties())
            {
                // Пропускаем ID свойство
                if (patchProperty.Name.Equals("Id", StringComparison.OrdinalIgnoreCase))
                    continue;

                var patchValue = patchProperty.GetValue(patchDto);

                // Если это Optional<T>, проверяем HasValue
                if (patchProperty.PropertyType.IsGenericType && 
                    patchProperty.PropertyType.GetGenericTypeDefinition() == typeof(Optional<>))
                {
                    var hasValueProperty = patchProperty.PropertyType.GetProperty("HasValue");
                    var valueProperty = patchProperty.PropertyType.GetProperty("Value");

                    if (hasValueProperty != null && valueProperty != null)
                    {
                        var hasValue = (bool)hasValueProperty.GetValue(patchValue);
                        if (hasValue)
                        {
                            var value = valueProperty.GetValue(patchValue);
                            var entityProperty = entityType.GetProperty(patchProperty.Name.Replace("Optional", ""));
                            entityProperty?.SetValue(entity, value);
                        }
                    }
                }
                // Если это обычное свойство и оно не null
                else if (patchValue != null)
                {
                    var entityProperty = entityType.GetProperty(patchProperty.Name);
                    entityProperty?.SetValue(entity, patchValue);
                }
            }
        }

        /// <summary>
        /// Создает частичное обновление из анонимного объекта
        /// </summary>
        public static void ApplyPatch<T>(this T entity, object patch) where T : class
        {
            if (patch == null) return;

            var entityType = typeof(T);
            var patchProperties = patch.GetType().GetProperties();

            foreach (var patchProperty in patchProperties)
            {
                var entityProperty = entityType.GetProperty(patchProperty.Name, 
                    System.Reflection.BindingFlags.IgnoreCase | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);

                if (entityProperty != null && entityProperty.CanWrite)
                {
                    var value = patchProperty.GetValue(patch);
                    if (value != null || !entityProperty.PropertyType.IsValueType)
                    {
                        entityProperty.SetValue(entity, value);
                    }
                }
            }
        }
    }
}