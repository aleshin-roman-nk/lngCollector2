using ThoughtzLand.Core.Models.Location;
using ThoughtzLand.Core.Models.Location.dto;
using ThoughtzLand.Core.Repos.Common;

namespace ThoughtzLand.Core.Repos
{
    public interface INodeRepo: IRepository<Node>
    {
        IEnumerable<Node> GetByTerrainId(int terrainId);
        NodeDetailDto GetDetail(int nodeId);
    }
}