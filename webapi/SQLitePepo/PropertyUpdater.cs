using ThoughtzLand.Core.Models.Common;
using ThoughtzLand.Core.Repos.Common;

namespace ThoughtzLand.ImplementRepo.SQLitePepo
{
    public class PropertyUpdater<TEntity>
        where TEntity: class, IDbEntity
    {
        private readonly AppData db;

        public PropertyUpdater(AppData db)
        {
            this.db = db;
        }

        public int UpdateString(int entId, string propname, string propvalue)
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

        public int UpdateInt(int entId, string propname, int propvalue)
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
