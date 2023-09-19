using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using ThoughtzLand.Core.Models.Common;
using ThoughtzLand.Core.Repos.Common;

namespace ThoughtzLand.ImplementRepo.SQLitePepo
{
	/// <summary>
	/// Обновление property by its name
	/// </summary>
	/// <typeparam name="TEntity">Dto type</typeparam>
    public class PropertyUpdaterByName<TEntity>: IPropertyUpdaterByName
		where TEntity: class, IDbEntity
    {
        private readonly AppData db;

        public PropertyUpdaterByName(AppData db)
        {
            this.db = db;
        }

		public int Update<TProperty>(int entId, string propname, TProperty propvalue)
		{
			if (entId == 0) throw new InvalidOperationException("empty object");

			try
			{
				var ent = db.Set<TEntity>().FirstOrDefault(x => x.id == entId);

				if (ent == null)
					throw new InvalidOperationException($"no object with id = {entId}");

				var entry = db.Entry(ent);
				entry.Property(propname).CurrentValue = propvalue;
				entry.Property(propname).IsModified = true;

				return db.SaveChanges();
			}
			catch (Exception)
			{
				throw;
			}
		}
	}
}
