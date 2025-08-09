using System.ComponentModel.DataAnnotations;
using ThoughtzLand.Core.Models.Common;

namespace ThoughtzLand.Core.Models.Exam.dto
{
    /// <summary>
    /// DTO для частичного обновления FlashCard
    /// Только заполненные поля будут обновлены
    /// </summary>
    public class PatchFlashCardDto : PatchDtoBase, IPatchable<FlashCard, PatchFlashCardDto>
    {
        /// <summary>
        /// Вопрос карточки
        /// </summary>
        [StringLength(1000, ErrorMessage = "Question cannot exceed 1000 characters")]
        public Optional<string?> Question { get; set; }

        /// <summary>
        /// Описание карточки
        /// </summary>
        [StringLength(2000, ErrorMessage = "Description cannot exceed 2000 characters")]
        public Optional<string?> Description { get; set; }

        /// <summary>
        /// ID языка
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "Language ID must be positive")]
        public Optional<int> LanguageId { get; set; }

        /// <summary>
        /// Количество правильных ответов подряд
        /// </summary>
        [Range(0, int.MaxValue, ErrorMessage = "Hits in row cannot be negative")]
        public Optional<int> HitsInRow { get; set; }

        /// <summary>
        /// Требуемое количество правильных ответов
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "Required hits must be positive")]
        public Optional<int> RequiredHits { get; set; }

        /// <summary>
        /// Общее количество попыток
        /// </summary>
        [Range(0, int.MaxValue, ErrorMessage = "Total hits cannot be negative")]
        public Optional<int> TotalHits { get; set; }

        /// <summary>
        /// Уровень сложности
        /// </summary>
        [Range(1, 10, ErrorMessage = "Level must be between 1 and 10")]
        public Optional<int> Level { get; set; }

        /// <summary>
        /// Дата следующего экзамена
        /// </summary>
        public Optional<DateTime> NextExamDate { get; set; }

        /// <summary>
        /// Статус завершения
        /// </summary>
        public Optional<bool> IsCompleted { get; set; }

        /// <summary>
        /// Стоимость квеста
        /// </summary>
        [Range(0, int.MaxValue, ErrorMessage = "Quest price cannot be negative")]
        public Optional<int> QuestPrice { get; set; }

        /// <summary>
        /// Применяет изменения к сущности FlashCard
        /// </summary>
        public void ApplyPatch(FlashCard entity, PatchFlashCardDto patchDto)
        {
            if (patchDto.Question.HasValue)
                entity.question = patchDto.Question.Value;

            if (patchDto.Description.HasValue)
                entity.description = patchDto.Description.Value;

            if (patchDto.LanguageId.HasValue)
                entity.languageId = patchDto.LanguageId.Value;

            if (patchDto.HitsInRow.HasValue)
                entity.hitsInRow = patchDto.HitsInRow.Value;

            if (patchDto.RequiredHits.HasValue)
                entity.requiredHits = patchDto.RequiredHits.Value;

            if (patchDto.TotalHits.HasValue)
                entity.totalHits = patchDto.TotalHits.Value;

            if (patchDto.Level.HasValue)
                entity.level = patchDto.Level.Value;

            if (patchDto.NextExamDate.HasValue)
                entity.nextExamDate = patchDto.NextExamDate.Value;

            if (patchDto.IsCompleted.HasValue)
                entity.isCompleted = patchDto.IsCompleted.Value;

            if (patchDto.QuestPrice.HasValue)
                entity.questPrice = patchDto.QuestPrice.Value;
        }
    }
}