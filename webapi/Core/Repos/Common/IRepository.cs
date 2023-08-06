using System.Linq.Expressions;
using ThoughtzLand.Core.Models.Common;

namespace ThoughtzLand.Core.Repos.Common
{
    public interface IRepository<TEntity>
        where TEntity : class, IDbEntity
    {
        TEntity GetById(int id);
        IEnumerable<TEntity> GetAll();
        TEntity Create(TEntity entity);
        TEntity Update(TEntity entity);
        void Remove(int entId);
    }
}
