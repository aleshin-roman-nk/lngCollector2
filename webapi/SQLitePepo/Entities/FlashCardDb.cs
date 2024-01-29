using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThoughtzLand.Core.Models.Common;

namespace ThoughtzLand.ImplementRepo.SQLitePepo.Entities
{
	[Table("FlashCards")]
	public class FlashCardDb
	{
		public int id { get; set; }
		public int nodeId { get; set; }
		public string? question { get; set; }
		public string? description { get; set; }
		public DateTime nextExamDate { get; set; }
		public ICollection<FlashCardAnswerDb> answers { get; set; } = new List<FlashCardAnswerDb>();
		public int languageId { get; set; }
		public Language? language { get; set; }
		public int hitsInRow { get; set; }
		public int requiredHits { get; set; }
		public int totalHits { get; set; }
		public int level { get; set; }
	}
}
