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
		private readonly NodeLevelCalculator nodeLevelCalculator;

		public NodeService(INodeRepo r) 
		{
			this._repo = r;
			nodeLevelCalculator = new NodeLevelCalculator();
		}

		public NodeDetailDto GetDetail(int nodeId)
		{
			var res = _repo.GetNodeDetail(nodeId);

			res.level = nodeLevelCalculator.calcNodeLevel(res.FlashCardsTitles.Sum(fc => fc.questPrice));

			return res;
		}

		public NodeTitleDto Create(CreateNodeDto node)
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
	}
}
