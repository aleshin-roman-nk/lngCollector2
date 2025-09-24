using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThoughtzLand.Core.Models.Common;
using ThoughtzLand.Core.Repos;

namespace ThoughtzLand.ImplementRepo.EFCoreRepo
{
	public class UserRepoEFCore : IUserRepo
	{
		private readonly AppData db;

		public UserRepoEFCore(AppData db)
		{
			this.db = db;
		}

		public User Create(User entity)
		{
			db.Users.Add(entity);
			db.SaveChanges();
			return entity;
		}

		public User Get(int id)
		{
			throw new NotImplementedException();
		}

		public User GetByEmail(string email)
		{
			return db.Users.FirstOrDefault(x => x.email.Equals(email));
		}

		public User GetByName(string name)
		{
			return db.Users.FirstOrDefault(x => x.name.Equals(name));
		}

		public User Update(User entity)
		{
			throw new NotImplementedException();
		}

		public bool EmailExists(string email)
		{
			return db.Users.Any(u => u.email.ToLower().Equals(email.ToLower()));
		}
	}
}
