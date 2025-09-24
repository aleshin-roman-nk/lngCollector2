using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThoughtzLand.Core.Models.Common;

namespace ThoughtzLand.EFCoreRepo.Entities
{
	[Table("FlashCardAnswers")]
	public class FlashCardAnswerDb
	{
		public int id { get; set; }
		public int cardId { get; set; }
		public string? text { get; set; }
		[ForeignKey("language")]
		public int languageId { get; set; }
		public Language? language { get; set; }
	}
}
