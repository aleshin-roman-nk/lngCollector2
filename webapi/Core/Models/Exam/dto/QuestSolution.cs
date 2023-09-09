namespace ThoughtzLand.Core.Models.Exam.dto
{
    public class QuestSolution
    {
        public int thoughtId { get; set; }
        // Ищем выражение на заданном языке в коллекции thought
        // если выражение найдено, перемещаем его карточку в сл ячейку
        // в карточке добавляем очко
        public string? solution { get; set; }
        public int answeredLngId {  get; set; }
    }
}
