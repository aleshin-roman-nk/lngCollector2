using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ThoughtzLand.Core.Models.Location;
using ThoughtzLand.Core.Models.Location.dto;
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

		public NodeDetailDto GetDetail(int nodeId)
		{
			return _repo.GetDetail(nodeId);
		}

		public Node Create(Node node)
		{
			return _repo.Create(node);
		}

		public void Update(UpdateNodeNameAndDescriptionDto node)
		{
			_repo.Update(node);
		}

		public void Remove(int nodeId)
		{
			_repo.Remove(nodeId);
		}

		public IEnumerable<Node> GetByTerrainId(int terrainId)
		{
			return _repo.GetByTerrainId(terrainId);
		}
	}
}
