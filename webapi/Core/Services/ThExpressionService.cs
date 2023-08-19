using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThoughtzLand.Core.Models.Thoughts;
using ThoughtzLand.Core.Repos;

namespace ThoughtzLand.Core.Services
{
    public class ThExpressionService
    {
        private readonly IThExpressionRepo _repo;

        public ThExpressionService(IThExpressionRepo r)
        {
            this._repo = r;
        }

        public OperationResult Remove(int id)
        {
            try
            {
                if (_repo.HasWork(id))
                {
                    return new OperationResult(false, "this object has commited work which you can't forget");
                }

                _repo.Remove(id);
                return new OperationResult(true, "success");
            }
            catch (Exception e)
            {
                return new OperationResult(false, e.Message);
            }
        }

        public OperationResult<ThExpression?> GetById(int id)
        {
            try
            {
                var res = _repo.GetById(id);
                return new OperationResult<ThExpression?>(true, "success", res);
            }
            catch (Exception e)
            {
                return new OperationResult<ThExpression?>(false, e.Message, null);
            }
        }

        public OperationResult<IEnumerable<ThExpression>?> GetByThoughtId(int thoughtId)
        {
            try
            {
                var res = _repo.GetByThoughtId(thoughtId);
                return new OperationResult<IEnumerable<ThExpression>?> (true, "success", res);
            }
            catch (Exception e)
            {
                return new OperationResult<IEnumerable<ThExpression>?> (false, e.Message, null);
            }
        }

        public OperationResult UpdateString(int id, string propname, string propvalue)
        {
            try
            {
                _repo.UpdateString(id, propname, propvalue);
                return new OperationResult(true, "success");
            }
            catch (Exception e)
            {
                return new OperationResult(false, e.Message);
            }
        }

        public OperationResult<ThExpression?> Add(ThExpression o) 
        {
            try
            {
                var res = _repo.Create(o);
                return new OperationResult<ThExpression?>(true, "success", res);
            }
            catch (Exception e)
            {
                return new OperationResult<ThExpression?>(false, e.Message, null);
            }
        }
    }
}
