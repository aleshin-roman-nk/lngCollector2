using Microsoft.EntityFrameworkCore;
using Models;
using Models.Location;
using Models.Thought;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.repo
{
    public class ThoughtRepo : IThoughtRepo
    {
        private readonly IDbFactory _factory;

        public ThoughtRepo(IDbFactory factory)
        {
            this._factory = factory;
        }

        public IEnumerable<Thought> Get(int nodeId)
        {
            using (var db = _factory.Create())
            {
                return db.Thoughts.Include(x => x.expressions).Where(x => x.nodeId == nodeId).ToArray();
            }
        }

        public Thought? GetThought(int thId)
        {
            using (var db = _factory.Create())
            {
                return db.Thoughts.FirstOrDefault(x => x.id == thId);
            }
        }

        public Thought Create(int nodeId, Thought th)
        {
            using (var db = _factory.Create())
            {
                th.id = 0;

                th.nodeId = nodeId;
                th.createdDate = DateTime.Now;

                db.Thoughts.Add(th);
                db.SaveChanges();
                return th;
            }
        }

        /*
         * Как обновить и вообще менеджить.
         * 
         * В отдельности создаю ThExpression с указанием thoughtId
         * 
         * Удаление ThExpression из коллекции
         * - просто удалить объект
         * 
         * Добавление в коллекцию
         * - просто создать объект с присвоением thExpression.thoughtId = thoughtId
         * 
         * Для ThExpression свой ThExpressionRepo
         * 
         * 
         */

        public Thought Update(int thId, Thought th)
        {
            using (var db = _factory.Create())
            {
                try
                {
                    var _th = db.Thoughts.FirstOrDefault(x => x.id == thId);
                    if (_th != null)
                    {
                        _th.text = th.text;
                        _th.description = th.description;
                        db.SaveChanges();
                        return _th;
                    }

                    throw new InvalidOperationException($"Not such Thought in db with id = {thId}");
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException(ex.Message);
                }
            }
        }

        public void Delete(int thId)
        {
            using (var db = _factory.Create())
            {
                db.Thoughts.Remove(new Thought { id = thId });
                db.SaveChanges();
            }
        }
    }
}
