using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThoughtzLand.Core.Repos.Common
{
	public interface IRepoGetterAll<TEntity>
		where TEntity : class
	{
		IEnumerable<TEntity> GetAll();
	}
}
