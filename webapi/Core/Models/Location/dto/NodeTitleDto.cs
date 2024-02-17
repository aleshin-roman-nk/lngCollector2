using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThoughtzLand.Core.Models.Location.dto
{
	public class NodeTitleDto
	{
		public int id { get; set; }
		public int terrainId { get; set; }
		public string? name { get; set; }
		public string? description { get; set; }
		public int x { get; set; }
		public int y { get; set; }
		public int width { get; set; }
		public int height { get; set; }

		public int questCount { get; set; }
		public int completedQuestCount {  get; set; }
		public int questPrice { get; set; }
		public int completedQuestPrice { get; set; }
		public int level { get; set; }
	}
}
