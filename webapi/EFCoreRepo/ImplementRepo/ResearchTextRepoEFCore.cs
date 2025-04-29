using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThoughtzLand.Core.Models.Location.dto;
using ThoughtzLand.Core.Models.Location.src;
using ThoughtzLand.Core.Repos;

namespace ThoughtzLand.ImplementRepo.EFCoreRepo
{
	public class ResearchTextRepoEFCore : IResearchTextRepo
	{
		private readonly AppData db;

		public ResearchTextRepoEFCore(AppData db)
		{
			this.db = db;
		}
		public ResearchText Create(CreateResearchTextDto entity)
		{
			var res = db.ResearchTexts.Add(new ResearchText { nodeId = entity.nodeId, text = entity.text });
			db.SaveChanges();
			return res.Entity;
		}

		public void Remove(int id)
		{
			// Find the entity by its ID
			var entityToDelete = db.ResearchTexts.Find(id);

			if (entityToDelete != null)
			{
				// Remove the entity from the context
				db.ResearchTexts.Remove(entityToDelete);

				// Save changes to the database
				db.SaveChanges();
			}
			else
			{
				throw new InvalidOperationException($"No text block with id {id} in data base");
			}
		}

		public void Update(UpdateResearchTextDto dto)
		{
			// Find the existing ResearchText entity by its ID
			var existingResearchText = db.ResearchTexts.Find(dto.id);

			if (existingResearchText != null)
			{
				// Update properties with values from the DTO
				existingResearchText.text = dto.text;

				// Save changes to the database
				db.SaveChanges();
			}
			else
			{
				// Handle the case where the entity with the specified ID was not found
				throw new InvalidOperationException($"No text block with id {dto.id} in data base");
			}
		}
	}
}
