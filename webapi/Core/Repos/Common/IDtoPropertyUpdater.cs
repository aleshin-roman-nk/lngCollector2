using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ThoughtzLand.Core.Models.Common;

namespace ThoughtzLand.Core.Repos.Common
{
    public interface IDtoPropertyUpdater<TEntity>
		where TEntity : class, IDbEntity
    {
		int UpdateProperties(TEntity ent, params Expression<Func<TEntity, object>>[] propSelectors);
		int UpdateProperty<TProperty>(TEntity ent, Expression<Func<TEntity, TProperty>> propSelector);
	}
}
