using Models.Thought;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Location.dto
{
    public class NodeDetail
    {
        public NodeDetail(Node n, IEnumerable<Thought.Thought> ths)
        {
            Node = n;
            Thoughts = ths;
        }

        public Node Node { get; }
        public IEnumerable<Thought.Thought> Thoughts { get; }
    }
}
