using ThoughtzLand.Core.Models.Exam;
using ThoughtzLand.Core.Models.Location.src;

namespace ThoughtzLand.Core.Models.Location.dto
{
	public class NodeDetailDto
	{
		public int id { get; set; }
		public int terrainId { get; set; }
		public string? name { get; set; }
		public string? description { get; set; }
		public int level { get; set; }
		public IEnumerable<FlashCardTitle>? FlashCardsTitles { get; set; }
		public IEnumerable<ResearchText>? ResearchTexts { get; set; }
	}
}
