using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThoughtzLand.Core.Models.Exam;
using ThoughtzLand.Core.Repos;

namespace ThoughtzLand.ImplementRepo.SQLitePepo
{
	public class BoxCellRepo : IBoxCellRepo
	{
		private readonly AppData db;

		public BoxCellRepo(AppData db)
		{
			this.db = db;
		}

		public BoxCell Create(BoxCell entity)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<BoxCell> GetAll()
		{
			return db.BoxCells.OrderBy(x => x.cellName).ToArray();
		}

		public BoxCell GetById(int id)
		{
			throw new NotImplementedException();
		}

		public BoxCell GetFirstCell()
		{
			return db.BoxCells.OrderBy(x => x.cellName).FirstOrDefault();
		}

		public void Remove(int entId)
		{
			throw new NotImplementedException();
		}

		public BoxCell Update(BoxCell entity)
		{
			throw new NotImplementedException();
		}
	}
}
