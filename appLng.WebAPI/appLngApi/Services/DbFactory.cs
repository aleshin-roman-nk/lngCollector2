using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class DbFactory 
    {
        private readonly string path;

        public DbFactory(string path)
        {
            this.path = path;
        }

        public AppData Create()
        {
            return new AppData(path);
        }

    }

}
