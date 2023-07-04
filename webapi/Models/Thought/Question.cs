using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Thought
{
    public class Question
    {
        public int thoughtId { get; set; }
        public IEnumerable<ThExpression> expressions { get; set; }
    }
}
