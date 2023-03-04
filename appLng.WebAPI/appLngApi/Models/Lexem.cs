using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Lexem
    {
        public int id { get; set; }
        public string? text { get; set; }
        public int testMissionId { get; set; }
        public LexemType LexemType { get; set; }
    }
}
