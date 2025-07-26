using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Data;
using ThoughtzLand.Core.Models.Location;
using ThoughtzLand.Core.Models.Location.dto;
using ThoughtzLand.Core.Repos;
using ThoughtzLand.Core.Repos.Common;


namespace ThoughtzLand.ImplementRepo.EFCoreRepo
{
	public class TerrainRepoEFCore : ITerrainRepo
	{
		private readonly AppData db;
		private readonly IAuthorizedUserService authUserServ;
		PropertyUpdater<TerrainDb> propertyUpdater;

		public TerrainRepoEFCore(AppData db, IAuthorizedUserService authUserServ)
		{
			this.db = db;
			this.authUserServ = authUserServ;
			propertyUpdater = new PropertyUpdater<TerrainDb>(db);
		}

		public IEnumerable<TerrainTitleDto> GetAllTerrainTitles()
		{
			var user = authUserServ.Get();
			return db.Terrains
				.Where(t => t.userId == int.Parse(user.Id))
				.Select(t => new TerrainTitleDto
				{
					description = t.description,
					id = t.id,
					name = t.name,
					userId = t.userId
				});
		}

		public TerrainDetailDto GetTerrainDetail(int id)
		{
			var terrainDetailDto = db.Terrains
				.Where(t => t.id == id)
				.Select(t => new TerrainDetailDto
				{
					id = t.id,
					name = t.name,
					description = t.description,
					userId = t.userId,
					nodes = t.Nodes
						.Where(n => n.terrainId == t.id)
						.Select(n => new NodeTitleDto
						{
							id = n.id,
							terrainId = n.terrainId,
							name = n.name,
							description = n.description,
							x = n.x,
							y = n.y,
							width = n.width,
							height = n.height,
							//questsMinimumTotalPrice = n.questsMinimumTotalPrice,
							questCount = n.FlashCards.Count(),// + n.Stories.Count(),
							completedQuestCount = n.FlashCards.Count(fc => fc.isCompleted),
							questPrice = n.FlashCards.Sum(fc => fc.questPrice),// + n.Stories.Sum(s => s.points),
							completedQuestPrice = n.FlashCards.Where(fc => fc.isCompleted).Sum(fc => fc.questPrice)
						})
				})
				.FirstOrDefault();

			if (terrainDetailDto != null)
				return terrainDetailDto;
			else
				throw new InvalidOperationException($"no such terrain with id = {id}");
		}

		public TerrainTitleDto Create(CreateTerrainDto entity)
		{
			var newTerrain = new TerrainDb 
			{ 
				description = entity.description,
				name = entity.name,
				userId = entity.userId
			};

			db.Terrains.Add(newTerrain);
			var success = db.SaveChanges() > 0;
			if (success)
				return new TerrainTitleDto
				{
					id = newTerrain.id,
					description = newTerrain.description,
					userId = newTerrain.userId,
					name = newTerrain.name
				};
			else
				throw new InvalidOperationException("wrong with creating terrain");
		}

		public int UpdateProperty(int entityId, string propertyName, object propertyValue)
		{
			return propertyUpdater.UpdateProperty(entityId, propertyName, propertyValue);
		}

		public void Remove(int entId)
		{
			db.Terrains.Remove(new TerrainDb { id = entId });
			var success = db.SaveChanges() > 0;

			if (!success)
				throw new InvalidOperationException($"wrong with deleting terrain id = {entId}");
		}

		public void Update(UpdateTerrainDto entity)
		{
			var terrDb = db.Terrains.FirstOrDefault(terr => terr.id == entity.id);

			if (terrDb == null) throw new InvalidOperationException($"Not such terrain id = {entity.id}");

			terrDb.description = entity.description;
			terrDb.name = entity.name;

			db.Entry(terrDb).Property(x => x.description).IsModified = true;
			db.Entry(terrDb).Property(x => x.name).IsModified = true;

			db.SaveChanges();
		}
	}
}
