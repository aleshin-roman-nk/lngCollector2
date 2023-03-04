using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class LexemMeaning
    {
        public int id { get; set; }
        public int lexemId { get; set; }
        public string? text { get; set; }
        public string? description { get; set; }
    }
}
