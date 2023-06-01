using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Exam.dto
{
    public class TestingQuestionFrame
    {
        public int lexemId { get; set; }
        public string? text { get; set; }
        public int Total { get; set; }
        public int Left { get; set; }
    }
}
