using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ThoughtzLand.Core.Models.Location;
using ThoughtzLand.Core.Models.Location.dto;
using ThoughtzLand.Core.Models.Thoughts;
using ThoughtzLand.Core.Repos;

namespace ThoughtzLand.Core.Services
{
    public class NodeService
    {
        private readonly INodeRepo _repo;

        public NodeService(INodeRepo r) 
        {
            this._repo = r;
        }

        public OperationResult<NodeDetailDto?> GetDetail(int nodeId)
        {
            try
            {
                var res = _repo.GetDetail(nodeId);
                return new OperationResult<NodeDetailDto?>(true, "success", res);
            }
            catch (Exception e)
            {
                return new OperationResult<NodeDetailDto?>(false, e.Message, null);
            }
        }

        public OperationResult<Node?> Create(Node node)
        {
            try
            {
                var res = _repo.Create(node);
                return new OperationResult<Node?>(true, "success", res);
            }
            catch (Exception e)
            {
                return new OperationResult<Node?>(false, e.Message, null);
            }
        }

        public OperationResult<Node?> Update(Node node)
        {
            try
            {
                var res = _repo.Update(node);
                return new OperationResult<Node?>(true, "success", res);
            }
            catch (Exception e)
            {
                return new OperationResult<Node?>(false, e.Message, null);
            }
        }

        public OperationResult Remove(int nodeId)
        {
            try
            {
                _repo.Remove(nodeId);
                return new OperationResult(true, "success");
            }
            catch (Exception e)
            {
                return new OperationResult(false, e.Message);
            }
        }

        public OperationResult<IEnumerable<Node>?> GetByTerrainId(int terrainId)
        {
            try
            {
                var res = _repo.GetByTerrainId(terrainId);
                return new OperationResult<IEnumerable<Node>?>(true, "success", res);
            }
            catch (Exception e)
            {
                return new OperationResult<IEnumerable<Node>?>(false, e.Message, null);
            }
        }
    }
}
