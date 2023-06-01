using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Location
{
    public class Node
    {
        public int id { get; set; }
        public int terrianId { get; set; }
        public string? name { get; set; }
        public string? description { get; set; }
        //public IEnumerable<IMission>? Missions { get; set; }// грузим при открытии объекта
        public int x { get; set; }// при создании, на сервере спрашиваем у территории последние доступное место
        public int y { get; set; }
    }
}
