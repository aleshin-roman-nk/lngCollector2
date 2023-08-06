namespace ThoughtzLand.Core.Models.Exam
{
    // Для вычисления среднего балла Node
    // Результат прохождения всей коллекции вопросов
    public class Grade
    {
        public int id { get; set; }
        public int nodeId { get; set; }
        public decimal value { get; set; }
        public DateTime Date { get; set; }
    }
}
