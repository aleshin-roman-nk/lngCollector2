using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserRegistry
{
	public class AuthorizedUser
	{
		public string? Name { get; }
		public string? Email { get; }
		public string? Id { get; }

		public AuthorizedUser(string? id, string? name, string? email)
		{
			Id = id;
			Name = name;
			Email = email;
		}
	}
}
