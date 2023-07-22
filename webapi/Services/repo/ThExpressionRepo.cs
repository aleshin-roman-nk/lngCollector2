using Models.Thought;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.repo
{
    public class ThExpressionRepo : IThExpressionRepo
    {
        private readonly IDbFactory _factory;

        public ThExpressionRepo(IDbFactory factory)
        {
            this._factory = factory;
        }

        public IEnumerable<ThExpression> GetAllOf(int thoughtId)
        {
            using (var db = _factory.Create())
            {
                return db.ThExpressions.Where(x => x.thoughtId == thoughtId).ToArray();
            }
        }

        public ThExpression Create(int thoughtId, ThExpression thexp)
        {
            using (var db = _factory.Create())
            {
                thexp.id = 0;

                thexp.createdDate = DateTime.Now;
                thexp.thoughtId = thoughtId;

                db.ThExpressions.Add(thexp);
                db.SaveChanges();
                return thexp;
            }
        }

        public void AddScore(int expId, int scores)
        {
            using (var db = _factory.Create())
            {
                var exp = db.ThExpressions.FirstOrDefault(x => x.id == expId);

                if (exp != null)
                {
                    exp.scores += scores;
                    db.SaveChanges();
                }
            }
        }

        public ThExpression UpdateText(int id, string propname, string newtext)
        {
            using (var db = _factory.Create())
            {
                try
                {
                    var exp = db.ThExpressions.FirstOrDefault(x => x.id == id);
                    if (exp != null)
                    {
                        var entry = db.Entry(exp);
                        entry.Property(propname).CurrentValue = newtext;
                        entry.Property(propname).IsModified = true;

                        db.SaveChanges();
                        return exp;
                    }

                    throw new InvalidOperationException($"Not such ThExpression in db with id = {id}");
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException(ex.Message);
                }
            }
        }

        public ThExpression GetOne(int id)
        {
            using (var db = _factory.Create())
            {
                return db.ThExpressions.FirstOrDefault(x => x.id == id);
            }
        }

        public RepoOperationResult Delete(int id)
        {
            using (var db = _factory.Create())
            {
                db.ThExpressions.Remove(new ThExpression { id = id });

                bool sc;

                try
                {
                    sc = db.SaveChanges() > 0;
                }
                catch (Exception e)
                {
                    return new RepoOperationResult(e.Message, false);
                }

                return new RepoOperationResult(sc ? "Deleted successfully" : "Something went wrong while deleting", sc);
            }
        }
    }
}
