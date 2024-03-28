using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThoughtzLand.ImplementRepo.MySqlRepo.Entities.Nodes;

namespace ThoughtzLand.ImplementRepo.MySqlRepo.Entities.Terrains
{
	[Table("Terrains")]
	public class TerrainDb
	{
		public int id { get; set; }
		public string? name { get; set; }
		public string? description { get; set; }
		public int userId { get; set; }
		public IEnumerable<NodeDb>? Nodes { get; set; }
	}
}
