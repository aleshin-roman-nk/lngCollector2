using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThoughtzLand.Core.Models.Common;

namespace ThoughtzLand.Core.Models.Exam
{
	public class FlashCardAnswer
	{
		public int id { get; set; }
		public int cardId { get; set; }
		public string? text { get; set; }
		public Language? language { get; set; }
	}
}
