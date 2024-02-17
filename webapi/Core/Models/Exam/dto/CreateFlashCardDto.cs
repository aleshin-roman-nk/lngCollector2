using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThoughtzLand.Core.Models.Exam.dto
{
	public class CreateFlashCardDto
	{
		public int nodeId {  get; set; }
		public string? question {  get; set; }
		public string? description { get; set; }
		public int languageId {  get; set; }
	}
}
