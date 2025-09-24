using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThoughtzLand.Core.Services
{
	internal class NodeLevelCalculator
	{
		public int calcNodeLevel(int nodeTotalPrice)
		{
			if (nodeTotalPrice < 50) return 0;
			if (nodeTotalPrice >= 50 && nodeTotalPrice < 100) return 1;
			if (nodeTotalPrice >= 100 && nodeTotalPrice < 250) return 2;
			if (nodeTotalPrice >= 250 && nodeTotalPrice < 400) return 3;
			//if (nodeTotalPrice >= 400) return 4;
			return 4;
		}
	}
}
