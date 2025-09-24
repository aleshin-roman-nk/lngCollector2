using ThoughtzLand.Core.Models.Location;
using ThoughtzLand.Core.Models.Location.dto;
using ThoughtzLand.Core.Repos.Common;

namespace ThoughtzLand.Core.Repos
{
    public interface ITerrainRepo:
        IRepoCreator<CreateTerrainDto, TerrainTitleDto>,
		IRepoEntityByDtoUpdater<UpdateTerrainDto>,
        IRepoPropertyUpdater,
        IRepoRemover
    {
        IEnumerable<TerrainTitleDto> GetAllTerrainTitles();
        TerrainDetailDto GetTerrainDetail(int id);
	}
}