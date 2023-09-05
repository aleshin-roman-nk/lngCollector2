using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThoughtzLand.Core.Models.Common;
using ThoughtzLand.Core.Models.Thoughts;

namespace ThoughtzLand.Core.Models.Exam
{
	public class FlashCard: IDbEntity
	{
		public int id { get; set; }
		public ThExpression? expression { get; set; }
		public int SpacedRepetitionBoxCellId { get; set; }
		public DateTime LastExam { get; set; }
		public DateTime NextExamDate { get; set; }
		public int rightSolutionScores { get; set; }
	}
}
