using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThoughtzLand.Core.Models.Exam.dto
{
	public class CreateFlashCardCoreDto
	{
		public int nodeId { get; set; }
		public string? question { get; set; }
		public string? description { get; set; }
		public int languageId { get; set; }
		public DateTime nextExamDate { get; set; }
		public int requiredHits { get; set; }
		public int completedQuestPrice { get; set; }
		public int level { get; set; }
		public bool isCompleted { get; set; }
	}
}
