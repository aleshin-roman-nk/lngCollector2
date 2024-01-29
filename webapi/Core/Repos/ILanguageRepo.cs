using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThoughtzLand.Core.Models.Common;
using ThoughtzLand.Core.Repos.Common;

namespace ThoughtzLand.Core.Repos
{
	public interface ILanguageRepo: 
		IRepoCreator<Language>, 
		IRepoGetterAll<Language>, 
		IRepoEntityUpdater<Language>
	{
	}
}
