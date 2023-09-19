namespace ThoughtzLand.Core.Models.Exam.dto
{

    /// <summary>
    /// Card solution
    /// </summary>
    public class CardSolution
    {
        public int cardId { get; set; }
        // Ищем выражение на заданном языке в коллекции thought
        // если выражение найдено, перемещаем его карточку в сл ячейку
        // в карточке добавляем очко
        public string? solution { get; set; }
    }
}
