using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThoughtzLand.Core.Models.Common;

namespace ThoughtzLand.Core.Models.Exam
{
	public class FlashCardTitle
	{
		public int id {  get; set; }
		public int nodeId {  get; set; }
		public string? question { get; set; }
		public string? lngTag { get; set; }
		public int hitsInRow { get; set; }
		public int requiredHits { get; set; }
		public int totalHits { get; set; }
		public int level { get; set; }
		public int answersCount { get; set; }
		public int questPrice { get; set; }
		public bool isCompleted { get; set; }
	}
}
