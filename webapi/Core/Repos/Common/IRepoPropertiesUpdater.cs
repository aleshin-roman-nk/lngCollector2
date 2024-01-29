using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ThoughtzLand.Core.Repos.Common
{
	public interface IRepoPropertiesUpdater<TEntity>
		where TEntity : class
	{
		int UpdateProperties(TEntity ent, params Expression<Func<TEntity, object>>[] propSelectors);
	}
}
