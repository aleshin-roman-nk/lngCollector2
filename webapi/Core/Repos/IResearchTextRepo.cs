using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThoughtzLand.Core.Models.Location.dto;
using ThoughtzLand.Core.Models.Location.src;
using ThoughtzLand.Core.Repos.Common;

namespace ThoughtzLand.Core.Repos
{
	public interface IResearchTextRepo:
		IRepoEntityByDtoUpdater<UpdateResearchTextDto>,
		IRepoCreator<CreateResearchTextDto, ResearchText>,
		IRepoRemover
	{
	}
}
