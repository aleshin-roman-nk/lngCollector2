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
		public OperationResult<IEnumerable<Language>?> GetAll()
		{
			try
			{
				var res = repo.GetAll();
				return new OperationResult<IEnumerable<Language>?>(true, "success", res);
			}
			catch (Exception e)
			{
				return new OperationResult<IEnumerable<Language>?>(false, e.Message, null);
			}
		}

		public OperationResult<Language?> Create(Language language)
		{
			try
			{
				var res = repo.Create(language);
				return new OperationResult<Language?>(true, "success", res);
			}
			catch (Exception e)
			{
				return new OperationResult<Language?>(false, e.Message, null);
			}
		}

		public OperationResult<Language?> Update(Language lng)
		{
			try
			{
				var res = repo.Update(lng);
				return new OperationResult<Language?>(true, "success", res);
			}
			catch (Exception e)
			{
				return new OperationResult<Language?>(false, e.Message, null);
			}
		}
	}
}
