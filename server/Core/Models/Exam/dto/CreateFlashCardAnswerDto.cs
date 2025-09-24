using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThoughtzLand.Core.Models.Exam.dto
{
    public class CreateFlashCardAnswerDto
    {
        public int cardId { get; set; }
        public int lngId { get; set; }
        public string text { get; set; }
    }
}
