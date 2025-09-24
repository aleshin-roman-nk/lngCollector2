using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThoughtzLand.Core.Models.Exam;
using ThoughtzLand.Core.Models.Location.src;

namespace ThoughtzLand.EFCoreRepo.Entities
{
	[Table("Nodes")]
	public class NodeDb
	{
		public int id { get; set; }
		public int terrainId { get; set; }
		public string? name { get; set; }
		public string? description { get; set; }
		public int x { get; set; }// при создании, на сервере спрашиваем у территории последние доступное место
		public int y { get; set; }
		public int width { get; set; }
		public int height { get; set; }

		//public bool completed { get; set; }

		public IEnumerable<FlashCardDb>? FlashCards { get; set; }
		public IEnumerable<ResearchText>? ResearchTexts { get; set; }
	}
}
