using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Grade
    {
        public int id { get; set; }
        public int missionId { get; set; }
        public decimal value { get; set; }
        public DateTime Date { get; set; }
    }
}
