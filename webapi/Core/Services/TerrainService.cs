using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThoughtzLand.Core.Models.Location;
using ThoughtzLand.Core.Models.Thoughts;
using ThoughtzLand.Core.Repos;

namespace ThoughtzLand.Core.Services
{
    public class TerrainService
    {
        private readonly ITerrainRepo _repo;

        public TerrainService(ITerrainRepo r) 
        {
            this._repo = r;
        }

        public OperationResult<IEnumerable<Terrain>?> GetAll()
        {
            try
            {
                var res = _repo.GetAll();
                return new OperationResult<IEnumerable<Terrain>?>(true, "success", res);
            }
            catch (Exception e)
            {
                return new OperationResult<IEnumerable<Terrain>?>(false, e.Message, null);
            }
        }

        public OperationResult<Terrain?> GetById(int id)
        {
            try
            {
                var res = _repo.GetById(id);
                return new OperationResult<Terrain?>(true, "success", res);
            }
            catch (Exception e)
            {
                return new OperationResult<Terrain?>(false, e.Message, null);
            }
        }

        public OperationResult<Terrain?> Create(Terrain entity)
        {
            try
            {
                var res = _repo.Create(entity);
                return new OperationResult<Terrain?>(true, "success", res);
            }
            catch (Exception e)
            {
                return new OperationResult<Terrain?>(false, e.Message, null);
            }
        }

        public OperationResult<Terrain?> Update(Terrain entity)
        {
            try
            {
                var res = _repo.Update(entity);
                return new OperationResult<Terrain?>(true, "success", res);
            }
            catch (Exception e)
            {
                return new OperationResult<Terrain?>(false, e.Message, null);
            }
        }

        public OperationResult Remove(int entId)
        {
            try
            {
                _repo.Remove(entId);
                return new OperationResult(true, "success");
            }
            catch (Exception e)
            {
                return new OperationResult(false, e.Message);
            }
        }
    }
}
