using ThoughtzLand.Core.Models.Thoughts;

namespace ThoughtzLand.Core.Models.Location.dto
{
    public class NodeDetailDto
    {
        public NodeDetailDto(Node n, IEnumerable<Thought> ths)
        {
            Node = n;
            Thoughts = ths;
        }

        public Node Node { get; }
        public IEnumerable<Thought> Thoughts { get; }
    }
}
