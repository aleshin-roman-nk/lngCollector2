using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Services.repo
{
    public class RepositoryBase<TEntity>
        where TEntity: class, IDbEntity
    {
        protected readonly IDbFactory _factory;

        public RepositoryBase(IDbFactory factory)
        {
            this._factory = factory;
        }

        //public OperationResult UpdateString(IDbEntity o, string propname, string propvalue)
        public OperationResult UpdateString(int entId, string propname, string propvalue)
        {
            //if (o == null) return new OperationResult(false, "empty object");
            if (entId == 0) return new OperationResult(false, "empty object");

            using (var db = _factory.Create())
            {
                try
                {
                    //var ent = db.Set<TEntity>().FirstOrDefault(x => x.id == o.id);
                    var ent = db.Set<TEntity>().FirstOrDefault(x => x.id == entId);

                    //if(ent == null)
                    //    return new OperationResult(false, $"no object with id = {o.id}");
                    if (ent == null)
                        return new OperationResult(false, $"no object with id = {entId}");

                    var entry = db.Entry(ent);
                    entry.Property(propname).CurrentValue = propvalue;
                    entry.Property(propname).IsModified = true;

                    if (db.SaveChanges() > 0)
                        return new OperationResult(true, "operation is successful");
                    else
                        return new OperationResult(false, "something went wrong when updating a property");
                }
                catch (Exception ex)
                {
                    return new OperationResult(false, ex.Message);
                }
            }
        }
    }
}
