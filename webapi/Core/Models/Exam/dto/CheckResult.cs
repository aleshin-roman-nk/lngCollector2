namespace ThoughtzLand.Core.Models.Exam.dto
{
	public class CheckResult
	{
		public bool isCorrect { get; set; }
		public int cardId { get; set; }
		public DateTime? nextExamDate { get; set; }
		public int totalHits {  get; set; }
		public int hitsInRow { get; set; }
		public int level { get; set; }
		public bool isJustCompleted { get; set; }


		//public ICollection<string> answers { get; set; } = new List<string>();
	}
}
