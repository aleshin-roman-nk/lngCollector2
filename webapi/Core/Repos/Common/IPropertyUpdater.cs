using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThoughtzLand.Core.Repos.Common
{
    public interface IPropertyUpdater
    {
        void UpdateInt(int entId, string propname, int propvalue);
        void UpdateString(int entId, string propname, string propvalue);
    }
}
