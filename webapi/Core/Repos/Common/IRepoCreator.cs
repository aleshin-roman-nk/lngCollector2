using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThoughtzLand.Core.Repos.Common
{
	public interface IRepoCreator<TSourceEntity, TResultEntity>
		where TSourceEntity : class
		where TResultEntity : class
	{
		TResultEntity Create(TSourceEntity entity);
	}

	public interface IRepoCreator<TEntity>
	where TEntity : class
	{
		TEntity Create(TEntity entity);
	}
}
