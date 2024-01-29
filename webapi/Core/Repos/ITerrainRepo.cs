using ThoughtzLand.Core.Models.Location;
using ThoughtzLand.Core.Models.Location.dto;
using ThoughtzLand.Core.Repos.Common;

namespace ThoughtzLand.Core.Repos
{
    public interface ITerrainRepo:
        IRepoCreator<CreateTerrainDto, Terrain>,
		IRepoEntityByDtoUpdater<UpdateTerrainDto>,
        IRepoPropertyUpdater,
        IRepoGetterAll<Terrain>,
        IRepoGetterOneById<Terrain>,
        IRepoRemover
    {
    }
}