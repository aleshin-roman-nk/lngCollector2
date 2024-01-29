using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThoughtzLand.Core.Models.Location.dto;
using ThoughtzLand.Core.Models.Location.src;
using ThoughtzLand.Core.Repos;

namespace ThoughtzLand.Core.Services
{
	public class ResearchTextService
	{
		private readonly IResearchTextRepo repo;

		public ResearchTextService(IResearchTextRepo repo)
		{
			this.repo = repo;
		}

		public ResearchText Create(CreateResearchTextDto dto)
		{
			return repo.Create(dto);
		}
		public void Update(UpdateResearchTextDto dto)
		{
			repo.Update(dto);
		}

		public void Remove(int id)
		{
			repo.Remove(id);
		}
	}
}
