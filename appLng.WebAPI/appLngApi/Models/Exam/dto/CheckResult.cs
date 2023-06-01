using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Exam.dto
{
    public class CheckResult
    {
        public bool isCorrect { get; set; }
        public string[]? correctStrings { get; set; }
    }
}
