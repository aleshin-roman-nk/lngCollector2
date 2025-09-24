using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThoughtzLand.Core.Models.Common
{
	public class User
	{
		public int id { get; set; }
		public string? name { get; set; }
		public string? email { get; set; }
		public byte[] passwordHash { get; set; }
		public byte[] passwordSalt { get; set; }
		//public string refreshToken { get; set; } = string.Empty;
		//public DateTime tokenCreated { get; set; }
		//public DateTime tokenExpires { get; set; }
	}
}
