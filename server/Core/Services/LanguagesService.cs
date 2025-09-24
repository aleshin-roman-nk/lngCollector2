using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ThoughtzLand.Core.Models.Common;
using ThoughtzLand.Core.Models.Location;
using ThoughtzLand.Core.Repos;

namespace ThoughtzLand.Core.Services
{
	public class LanguagesService
	{
		private readonly ILanguageRepo repo;

		public LanguagesService(ILanguageRepo r)
		{
			this.repo = r;
		}
		public IEnumerable<Language> GetAll()
		{
			return repo.GetAll();
		}

		public Language Create(Language language)
		{
			return repo.Create(language);
		}

		public Language Update(Language lng)
		{
			return repo.Update(lng);
		}
	}
}
