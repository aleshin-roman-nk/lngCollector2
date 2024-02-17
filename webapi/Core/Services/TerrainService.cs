using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThoughtzLand.Core.Models.Location;
using ThoughtzLand.Core.Models.Location.dto;
using ThoughtzLand.Core.Repos;

namespace ThoughtzLand.Core.Services
{
	public class TerrainService
	{
		private readonly ITerrainRepo _repo;
		private readonly NodeLevelCalculator nodeLevelCalculator;

		public TerrainService(ITerrainRepo r) 
		{
			this._repo = r;
			nodeLevelCalculator = new NodeLevelCalculator();
		}

		public IEnumerable<TerrainTitleDto> GetAllTerrainTitles()
		{
			return _repo.GetAllTerrainTitles();
		}

		public TerrainDetailDto GetTerrainDetail(int id)
		{
			var res = _repo.GetTerrainDetail(id);

			foreach (var item in res.nodes) 
			{
				item.level = nodeLevelCalculator.calcNodeLevel(item.questPrice);
			}

			return res;
		}

		public TerrainTitleDto Create(CreateTerrainDto entity)
		{
			return _repo.Create(entity);
		}

		public void UpdateProperty(int entityId, string propertyName, object propertyValue)
		{
			_repo.UpdateProperty(entityId, propertyName, propertyValue);
		}

		public void Remove(int entId)
		{
			_repo.Remove(entId);
		}

		public void Update(UpdateTerrainDto dto)
		{
			_repo.Update(dto);
		}
	}
}
