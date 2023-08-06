using Microsoft.EntityFrameworkCore;
using ThoughtzLand.Core.Models.Thoughts;
using ThoughtzLand.Core.Repos;
using ThoughtzLand.Core.Services;

namespace ThoughtzLand.ImplementRepo.SQLitePepo
{
    public class ThExpressionRepo : IThExpressionRepo
    {
        private readonly AppData db;
        private PropertyUpdater<ThExpression> propertyUpdater;

        public ThExpressionRepo(AppData db)
        {
            this.db = db;
            propertyUpdater = new PropertyUpdater<ThExpression>(db);
        }

        public IEnumerable<ThExpression> GetByThoughtId(int thoughtId)
        {
            return db.ThExpressions.Where(x => x.thoughtId == thoughtId).ToArray();
        }

        public ThExpression Create(ThExpression thexp)
        {
            thexp.id = 0;
            thexp.createdDate = DateTime.Now;

            db.ThExpressions.Add(thexp);
            var success = db.SaveChanges() > 0;

            if (success)
                return thexp;
            else
                throw new InvalidOperationException("something went wrong when creating a expression");
        }

        public ThExpression GetById(int id)
        {
            var data = db.ThExpressions.FirstOrDefault(x => x.id == id);

            if (data != null) return data;

            throw new InvalidOperationException($"wrong with getting th-expression id = {id}");
        }

        public IEnumerable<ThExpression> GetAll()
        {
            throw new NotImplementedException();
        }

        public ThExpression Update(ThExpression entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(int entId)
        {
            db.Entry(new ThExpression { id = entId }).State = EntityState.Deleted;
            var success = db.SaveChanges() > 0;
            if (!success)
                throw new InvalidOperationException($"th-expression with id = {entId} coild not be deleted");
        }

        public void UpdateInt(int entId, string propname, int propvalue)
        {
            propertyUpdater.UpdateInt(entId, propname, propvalue);
        }

        public void UpdateString(int entId, string propname, string propvalue)
        {
            propertyUpdater.UpdateString(entId, propname, propvalue);
        }

        public bool HasWork(int id)
        {
            return db.ThExpressions.Where(x => x.id == id).Sum(x => x.scores) > 0;
        }
    }
}
