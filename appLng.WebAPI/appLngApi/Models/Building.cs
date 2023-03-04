using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Building
    {
        public int terrianId { get; set; }
        public string? name { get; set; }
        //public IEnumerable<IMission>? Missions { get; set; }// грузим при открытии объекта
        public int x { get; set; }// при создании, на сервере спрашиваем у территории последние доступное место
        public int y { get; set; }
    }
}
