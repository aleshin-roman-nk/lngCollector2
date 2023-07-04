using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Thought
{
    public class Thought
    {
        public Thought()
        {
            expressions = new List<ThExpression>();
        }

        public int id { get; set; }
        public int nodeId { get; set; }
        public string? text { get; set; }
        public string? description { get; set; }
        public DateTime? createdDate { get; set; }
        public ICollection<ThExpression> expressions { get; set; }
    }
}
