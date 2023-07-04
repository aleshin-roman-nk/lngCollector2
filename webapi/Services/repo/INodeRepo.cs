using Models.Location;
using Models.Location.dto;

namespace Services.repo
{
    public interface INodeRepo
    {
        Node Create(int terrId, Node node);
        void Delete(int nodeId);
        IEnumerable<Node> GetAllOf(int terrId);
        NodeDetail GetDetail(int nodeId);
        Node Update(int nodeId, Node node);
    }
}