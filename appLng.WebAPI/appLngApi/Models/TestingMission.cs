using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class TestingMission : IMission
    {
        public int id { get; set; }
        public string? name { get; set; }
        public int buildingId { get; set; }
        public string? description { get; set; }
    }
}
