using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThoughtzLand.Core.Models.Common;

namespace ThoughtzLand.Core.Models.Exam.dto
{
	public class UpdateCardAnswerDto
	{
		public int id { get; set; }
		public string? text { get; set; }
		public int languageId { get; set; }
	}
}
