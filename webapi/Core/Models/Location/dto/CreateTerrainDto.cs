using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThoughtzLand.Core.Models.Location.dto
{
	public class CreateTerrainDto
	{
		public string? name { get; set; }
		public string? description { get; set; }
		public int userId { get; set; }
	}
}
