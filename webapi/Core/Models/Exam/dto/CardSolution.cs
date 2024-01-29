namespace ThoughtzLand.Core.Models.Exam.dto
{

	/// <summary>
	/// Card solution
	/// </summary>
	public class CardSolution
	{
		public int cardId { get; set; }
		public string? solution { get; set; }
		public bool helpIsUsed {  get; set; }
	}
}
