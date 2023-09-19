using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThoughtzLand.Core.Models.Common;

namespace ThoughtzLand.Core.Models.Exam
{
	public class BoxCell : IDbEntity
	{
		public int id { get; set; }
		public int cellName { get; set; }
		public double nextIntervalInMinutes { get; set; }
	}
}
