using ThoughtzLand.Core.Models.Location;
using ThoughtzLand.Core.Models.Location.dto;
using ThoughtzLand.Core.Repos.Common;

namespace ThoughtzLand.Core.Repos
{
    public interface INodeRepo:
        IRepoCreator<CreateNodeDto, NodeTitleDto>,
        IRepoEntityByDtoUpdater<UpdateNodeNameAndDescriptionDto>,
        IRepoRemover
    {
        NodeDetailDto GetNodeDetail(int nodeId);
    }
}