using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Exam.dto
{
    public class SolutionCheckResult
    {
        public bool Completed { get; set; }
        public bool isCorrect { get; set; }
        public string[]? CorrectStrings { get; set; }
    }
}
