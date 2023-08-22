using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThoughtzLand.Core.Models.Common
{
	public class Language : IDbEntity
	{
		public int id { get; set; }
		public string name { get; set; }
		public string shortName { get; set; }
	}
}
