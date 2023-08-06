using Microsoft.EntityFrameworkCore;
using ThoughtzLand.Core.Models.Location;
using ThoughtzLand.Core.Models.Location.dto;
using ThoughtzLand.Core.Repos;

namespace ThoughtzLand.ImplementRepo.SQLitePepo
{
    public class NodeRepo : INodeRepo
    {
        private readonly AppData db;

        public NodeRepo(AppData db)
        {
            this.db = db;
        }

        public NodeDetailDto GetDetail(int nodeId)
        {
            var n = db.Nodes.FirstOrDefault(x => x.id == nodeId);
            if (n == null) { throw new InvalidOperationException($"no such node in db (id={nodeId})"); }

            var ths = db.Thoughts.Include(x => x.expressions).Where(x => x.nodeId == nodeId).ToList();

            return new NodeDetailDto(n, ths);
        }

        public Node Create(Node node)
        {
            node.id = 0;

            db.Nodes.Add(node);
            var success = db.SaveChanges() > 0;
            if (!success)
                throw new InvalidOperationException("something went wrong when updating a node");
            return node;
        }

        public Node Update(Node node)
        {
            try
            {
                var _node = db.Nodes.FirstOrDefault(x => x.id == node.id);
                if (_node != null)
                {
                    _node.name = node.name;
                    _node.description = node.description;
                    _node.y = node.y;
                    _node.x = node.x;

                    var success = db.SaveChanges() > 0;
                    if (!success)
                        throw new InvalidOperationException("something went wrong when updating a node");
                    return _node;
                }

                throw new InvalidOperationException($"Not such Node in db with id = {node.id}");
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public void Remove(int nodeId)
        {
            db.Nodes.Remove(new Node { id = nodeId });
            var success = db.SaveChanges() > 0;
            if (!success)
                throw new InvalidOperationException("something went wrong when deleting a node");
        }

        public IEnumerable<Node> GetByTerrainId(int terrainId)
        {
            return db.Nodes.Where(x => x.terrianId == terrainId).ToArray();
        }

        public Node GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Node> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
