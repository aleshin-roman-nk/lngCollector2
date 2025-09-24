using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThoughtzLand.Core.Repos.Common
{
	public interface IRepoPropertyUpdater
	{
		int UpdateProperty(int entId, string propname, object propvalue);
	}
}
