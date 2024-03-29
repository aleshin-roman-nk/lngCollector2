﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThoughtzLand.Core.Models.Common;
using ThoughtzLand.Core.Models.Location;
using ThoughtzLand.Core.Repos;

namespace ThoughtzLand.ImplementRepo.SQLitePepo
{
	public class LanguageRepoSQLite : ILanguageRepo
	{
		private readonly AppDataSQLite db;

		public LanguageRepoSQLite(AppDataSQLite db)
		{
			this.db = db;
		}

		public Language Create(Language entity)
		{
			entity.id = 0;

			db.Languages.Add(entity);
			var success = db.SaveChanges() > 0;
			if (!success)
				throw new InvalidOperationException("something went wrong when creating a language");
			return entity;
		}

		public IEnumerable<Language> GetAll()
		{
			return db.Languages.ToArray();
		}

		public Language Update(Language entity)
		{
			db.Update(entity);
			db.SaveChanges();
			return entity;
		}
	}
}
