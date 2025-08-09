using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ThoughtzLand.Core.Models.Exam.dto
{
    /// <summary>
    /// JSON Patch операция согласно RFC 6902
    /// </summary>
    public class JsonPatchOperation
    {
        /// <summary>
        /// Тип операции: add, remove, replace, copy, move, test
        /// </summary>
        [Required]
        [JsonPropertyName("op")]
        public string Operation { get; set; } = string.Empty;

        /// <summary>
        /// Путь к свойству в формате JSON Pointer
        /// </summary>
        [Required]
        [JsonPropertyName("path")]
        public string Path { get; set; } = string.Empty;

        /// <summary>
        /// Значение для операции (не используется для remove)
        /// </summary>
        [JsonPropertyName("value")]
        public object? Value { get; set; }

        /// <summary>
        /// Исходный путь для операций copy и move
        /// </summary>
        [JsonPropertyName("from")]
        public string? From { get; set; }
    }

    /// <summary>
    /// DTO для частичного обновления FlashCard используя JSON Patch
    /// Пример использования:
    /// [
    ///   { "op": "replace", "path": "/question", "value": "New question" },
    ///   { "op": "replace", "path": "/hitsInRow", "value": 5 }
    /// ]
    /// </summary>
    public class JsonPatchFlashCardDto
    {
        /// <summary>
        /// ID обновляемой сущности
        /// </summary>
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// Список операций JSON Patch
        /// </summary>
        [Required]
        [MinLength(1, ErrorMessage = "At least one patch operation is required")]
        public List<JsonPatchOperation> Operations { get; set; } = new();

        /// <summary>
        /// Применяет JSON Patch операции к сущности FlashCard
        /// </summary>
        public void ApplyJsonPatch(FlashCard entity)
        {
            foreach (var operation in Operations)
            {
                ApplyOperation(entity, operation);
            }
        }

        private void ApplyOperation(FlashCard entity, JsonPatchOperation operation)
        {
            var propertyName = operation.Path.TrimStart('/');
            
            switch (operation.Operation.ToLower())
            {
                case "replace":
                case "add":
                    SetPropertyValue(entity, propertyName, operation.Value);
                    break;
                case "remove":
                    SetPropertyValue(entity, propertyName, GetDefaultValue(propertyName));
                    break;
                case "test":
                    ValidatePropertyValue(entity, propertyName, operation.Value);
                    break;
                default:
                    throw new NotSupportedException($"Operation '{operation.Operation}' is not supported");
            }
        }

        private void SetPropertyValue(FlashCard entity, string propertyName, object? value)
        {
            switch (propertyName.ToLower())
            {
                case "question":
                    entity.question = value?.ToString();
                    break;
                case "description":
                    entity.description = value?.ToString();
                    break;
                case "languageid":
                    if (int.TryParse(value?.ToString(), out int languageId))
                        entity.languageId = languageId;
                    break;
                case "hitsinrow":
                    if (int.TryParse(value?.ToString(), out int hitsInRow))
                        entity.hitsInRow = hitsInRow;
                    break;
                case "requiredhits":
                    if (int.TryParse(value?.ToString(), out int requiredHits))
                        entity.requiredHits = requiredHits;
                    break;
                case "totalhits":
                    if (int.TryParse(value?.ToString(), out int totalHits))
                        entity.totalHits = totalHits;
                    break;
                case "level":
                    if (int.TryParse(value?.ToString(), out int level))
                        entity.level = level;
                    break;
                case "nextexamdate":
                    if (DateTime.TryParse(value?.ToString(), out DateTime nextExamDate))
                        entity.nextExamDate = nextExamDate;
                    break;
                case "iscompleted":
                    if (bool.TryParse(value?.ToString(), out bool isCompleted))
                        entity.isCompleted = isCompleted;
                    break;
                case "questprice":
                    if (int.TryParse(value?.ToString(), out int questPrice))
                        entity.questPrice = questPrice;
                    break;
                default:
                    throw new ArgumentException($"Property '{propertyName}' is not supported for patching");
            }
        }

        private object? GetDefaultValue(string propertyName)
        {
            return propertyName.ToLower() switch
            {
                "question" or "description" => null,
                "languageid" or "hitsinrow" or "requiredhits" or "totalhits" 
                or "level" or "questprice" => 0,
                "nextexamdate" => DateTime.MinValue,
                "iscompleted" => false,
                _ => null
            };
        }

        private void ValidatePropertyValue(FlashCard entity, string propertyName, object? expectedValue)
        {
            var actualValue = propertyName.ToLower() switch
            {
                "question" => entity.question,
                "description" => entity.description,
                "languageid" => entity.languageId,
                "hitsinrow" => entity.hitsInRow,
                "requiredhits" => entity.requiredHits,
                "totalhits" => entity.totalHits,
                "level" => entity.level,
                "nextexamdate" => entity.nextExamDate,
                "iscompleted" => entity.isCompleted,
                "questprice" => entity.questPrice,
                _ => throw new ArgumentException($"Property '{propertyName}' is not supported for testing")
            };

            if (!Equals(actualValue, expectedValue))
            {
                throw new InvalidOperationException($"Test operation failed: expected {expectedValue}, got {actualValue}");
            }
        }
    }
}