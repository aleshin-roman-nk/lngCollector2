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
    public class ThoughtRepo : RepositoryBase<Thought>, IThoughtRepo
    {
        public ThoughtRepo(IDbFactory factory) : base(factory)
        {
        }

        //private readonly IDbFactory _factory;

        //public ThoughtRepo(IDbFactory factory)
        //{
        //    this._factory = factory;
        //}

        public OperationResult<Thought> Get(int id)
        {
            using (var db = _factory.Create())
            {
                var ent = db.Thoughts.Include(x => x.expressions).FirstOrDefault(x => x.id == id);

                if (ent != null)
                {
                    return new OperationResult<Thought>(true, "success", ent);
                }
                else return new OperationResult<Thought>(false, $"no object with it = { id }", null);
            }
        }

        //public Thought? GetThought(int thId)
        //{
        //    using (var db = _factory.Create())
        //    {
        //        return db.Thoughts.FirstOrDefault(x => x.id == thId);
        //    }
        //}

        public OperationResult<Thought> Create(int nodeId, Thought th)
        {
            using (var db = _factory.Create())
            {
                th.id = 0;

                th.nodeId = nodeId;
                th.createdDate = DateTime.Now;

                db.Thoughts.Add(th);
                var success = db.SaveChanges() > 0;
                if (success)
                    return new OperationResult<Thought>(true, "success", th);
                else
                    return new OperationResult<Thought>(false, "something went wrong when creating an object", null);
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

        //public OperationResult UpdateString(int thId, string name, string value)
        //{
        //    using (var db = _factory.Create())
        //    {
        //        try
        //        {
        //            var _th = db.Thoughts.FirstOrDefault(x => x.id == thId);
                    
        //            if (_th != null)
        //            {
        //                if(db.UpdateAndSaveString(_th, name, value))
        //                {
        //                    return new OperationResult(true, "operation is successful");
        //                }
        //            }

        //            return new OperationResult(false, $"Not such object of {this.GetType().Name} in db");
        //        }
        //        catch (Exception ex)
        //        {
        //            return new OperationResult(false, ex.Message);
        //        }
        //    }
        //}

        public OperationResult Delete(int thId)
        {
            using (var db = _factory.Create())
            {
                db.Thoughts.Remove(new Thought { id = thId });
                var success = db.SaveChanges() > 0;

                if (success)
                    return new OperationResult(true, $"object with id = {thId} is deleted");
                else
                    return new OperationResult(false, $"something went wrong when deleting an object id = {thId}");
            }
        }
    }
}
