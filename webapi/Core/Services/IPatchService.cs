using ThoughtzLand.Core.Models.Common;
using Microsoft.EntityFrameworkCore;

namespace ThoughtzLand.Core.Services
{
    /// <summary>
    /// Интерфейс сервиса для частичного обновления сущностей
    /// </summary>
    public interface IPatchService
    {
        /// <summary>
        /// Частично обновляет сущность
        /// </summary>
        /// <typeparam name="TEntity">Тип сущности</typeparam>
        /// <typeparam name="TPatchDto">Тип DTO для обновления</typeparam>
        /// <param name="patchDto">DTO с изменениями</param>
        /// <returns>Обновленная сущность</returns>
        Task<TEntity> PatchAsync<TEntity, TPatchDto>(TPatchDto patchDto) 
            where TEntity : class
            where TPatchDto : PatchDtoBase, IPatchable<TEntity, TPatchDto>, new();

        /// <summary>
        /// Частично обновляет сущность с валидацией
        /// </summary>
        /// <typeparam name="TEntity">Тип сущности</typeparam>
        /// <typeparam name="TPatchDto">Тип DTO для обновления</typeparam>
        /// <param name="patchDto">DTO с изменениями</param>
        /// <param name="validator">Функция валидации</param>
        /// <returns>Обновленная сущность</returns>
        Task<TEntity> PatchAsync<TEntity, TPatchDto>(TPatchDto patchDto, Func<TEntity, Task<bool>> validator = null) 
            where TEntity : class
            where TPatchDto : PatchDtoBase, IPatchable<TEntity, TPatchDto>, new();
    }
}