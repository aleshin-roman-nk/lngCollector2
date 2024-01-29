using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThoughtzLand.Core.Repos.Common
{
	public interface IRepoGetterOneById<TEntity>
		where TEntity : class
	{
		TEntity Get(int id);
	}
}
