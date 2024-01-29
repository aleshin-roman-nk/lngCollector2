using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ThoughtzLand.Core.Models.Common;
using ThoughtzLand.Core.Repos;

namespace ThoughtzLand.Core.Services
{
	public class UserService
	{
		private readonly IUserRepo userRepo;

		//public UserService(IUserRepo userRepo, IAuthorizedUser authorizedUser) { }
		public UserService(IUserRepo userRepo)
		{
			this.userRepo = userRepo;
		}

		public User Create(User user)
		{
			return userRepo.Create(user);
		}

		public User Update(User dto)
		{
			return userRepo.Update(dto);
		}

		public User GetUserByName(string name)
		{
			return userRepo.GetByName(name);
		}

		public User GetUserByEmail(string email)
		{
			return userRepo.GetByEmail(email);
		}

		public bool EmailExists(string email)
		{
			return userRepo.EmailExists(email);
		}
	}
}
