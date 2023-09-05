using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThoughtzLand.Core.Models.Thoughts;

namespace ThoughtzLand.ImplementRepo.SQLitePepo.Entities
{
	public class FlashCardDb
	{
		public int id { get; set; }
		[ForeignKey("expression")]
		public int expressionId { get; set; }
		public ThExpression? expression { get; set; }
		public int SpacedRepetitionBoxCellId { get; set; }
		public DateTime LastExam { get; set; }
		public DateTime NextExamDate { get; set; }
		public int rightSolutionScores { get; set; }
	}
}
