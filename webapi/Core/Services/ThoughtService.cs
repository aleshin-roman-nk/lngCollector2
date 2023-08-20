using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThoughtzLand.Core.Models.Thoughts;
using ThoughtzLand.Core.Repos;

namespace ThoughtzLand.Core.Services
{
    public class ThoughtService
    {
        private readonly IThoughtRepo _repo;

        public ThoughtService(IThoughtRepo r) 
        {
            this._repo = r;
        }

        public OperationResult Remove(int thoughtId)
        {
            try
            {
                if (_repo.ThoughtHasWork(thoughtId))
                {
                    return new OperationResult(false, "this object has commited work which you can't forget");
                }

                _repo.Remove(thoughtId);
                return new OperationResult(true, "success");
            }
            catch (Exception e)
            {
                return new OperationResult(false, e.Message);
            }
        }

        public OperationResult<Thought?> GetById(int thoughtId)
        {
            try
            {
                var res = _repo.GetById(thoughtId);
                return new OperationResult<Thought?>(true, "success", res);
            }
            catch (Exception e)
            {
                return new OperationResult<Thought?>(false, e.Message, null);
            }
        }

        public OperationResult<Thought?> Add(Thought th)
        {
            try
            {
                var res = _repo.Create(th);
                return new OperationResult<Thought?>(true, "success", res);
            }
            catch (Exception e)
            {
                return new OperationResult<Thought?>(false, e.Message, null);
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
    }
}
