using Models.Common;
using Models.Thought;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.repo
{
    public interface IRepository<TEntity>
    {
        //OperationResult UpdateString(IDbEntity o, string propname, string propvalue);
        OperationResult UpdateString(int entId, string propname, string propvalue);
        OperationResult<Thought> Create(int nodeId, TEntity ent);
        OperationResult Delete(int thId);
        OperationResult<Thought> Get(int id);
    }
}
