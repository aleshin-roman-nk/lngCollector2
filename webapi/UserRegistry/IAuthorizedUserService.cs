using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserRegistry
{
	public interface IAuthorizedUserService
	{
		AuthorizedUser Get();
	}
}
