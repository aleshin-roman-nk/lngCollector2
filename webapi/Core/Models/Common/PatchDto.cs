using System.Text.Json.Serialization;

namespace ThoughtzLand.Core.Models.Common
{
    /// <summary>
    /// Обертка для опциональных значений в DTO для частичного обновления
    /// </summary>
    /// <typeparam name="T">Тип значения</typeparam>
    public struct Optional<T>
    {
        private readonly T _value;
        private readonly bool _hasValue;

        [JsonConstructor]
        public Optional(T value)
        {
            _value = value;
            _hasValue = true;
        }

        public bool HasValue => _hasValue;
        public T Value => _hasValue ? _value : throw new InvalidOperationException("Optional has no value");

        public static implicit operator Optional<T>(T value) => new(value);
        
        public T GetValueOrDefault(T defaultValue = default) => _hasValue ? _value : defaultValue;
    }

    /// <summary>
    /// Базовый класс для DTO частичного обновления
    /// </summary>
    public abstract class PatchDtoBase
    {
        /// <summary>
        /// ID обновляемой сущности
        /// </summary>
        public int Id { get; set; }
    }

    /// <summary>
    /// Интерфейс для применения частичного обновления
    /// </summary>
    /// <typeparam name="TEntity">Тип сущности</typeparam>
    /// <typeparam name="TPatchDto">Тип DTO для обновления</typeparam>
    public interface IPatchable<TEntity, TPatchDto> 
        where TPatchDto : PatchDtoBase
    {
        /// <summary>
        /// Применяет изменения из DTO к сущности
        /// </summary>
        /// <param name="entity">Сущность для обновления</param>
        /// <param name="patchDto">DTO с изменениями</param>
        void ApplyPatch(TEntity entity, TPatchDto patchDto);
    }
}