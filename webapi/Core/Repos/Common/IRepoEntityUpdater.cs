using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThoughtzLand.Core.Repos.Common
{
	public interface IRepoEntityUpdater<TEntity>
		where TEntity : class
	{
		TEntity Update(TEntity entity);
	}

	public interface IRepoEntityUpdater<TSourceEntity, TResultEntity>
			where TSourceEntity : class
			where TResultEntity : class
	{
		TResultEntity Update(TSourceEntity entity);
	}
}
