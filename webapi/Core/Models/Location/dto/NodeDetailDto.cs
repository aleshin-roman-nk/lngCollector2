using ThoughtzLand.Core.Models.Exam;
using ThoughtzLand.Core.Models.Location.src;

namespace ThoughtzLand.Core.Models.Location.dto
{
	public class NodeDetailDto
	{
		public NodeDetailDto(Node n, IEnumerable<FlashCardTitle> ths, IEnumerable<ResearchText> rtxts)
		{
			Node = n;
			FlashCardsTitles = ths;
			ResearchTexts = rtxts;
		}

		public Node Node { get; }
		public IEnumerable<FlashCardTitle> FlashCardsTitles { get; }
		public IEnumerable<ResearchText> ResearchTexts { get; }
	}
}
