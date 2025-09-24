using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThoughtzLand.Core.Models.Location.dto
{
	public class CreateNodeDto
	{
		public int terrainId {  get; set; }
		public string? name { get; set; }
		public string? description { get; set; }
	}
}
