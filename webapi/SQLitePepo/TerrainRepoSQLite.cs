using Microsoft.Extensions.Logging;
using System.Data;
using ThoughtzLand.Core.Models.Location;
using ThoughtzLand.Core.Models.Location.dto;
using ThoughtzLand.Core.Repos;
using ThoughtzLand.Core.Repos.Common;
using UserRegistry;

namespace ThoughtzLand.ImplementRepo.SQLitePepo
{
	public class TerrainRepoSQLite : ITerrainRepo
	{
		private readonly AppData db;
		private readonly IAuthorizedUserService authUserServ;
		PropertyUpdater<Terrain> propertyUpdater;

		public TerrainRepoSQLite(AppData db, IAuthorizedUserService authUserServ)
		{
			this.db = db;
			this.authUserServ = authUserServ;
			propertyUpdater = new PropertyUpdater<Terrain>(db);

		}

		public IEnumerable<Terrain> GetAll()
		{
			var user = authUserServ.Get();
			return db.Terrains.Where(t => t.userId == int.Parse(user.Id));
		}

		public Terrain Get(int id)
		{
			var res = db.Terrains.FirstOrDefault(x => x.id == id);

			if (res != null)
				return res;
			else
				throw new InvalidOperationException($"no such terrain with id = {id}");
		}

		public Terrain Create(CreateTerrainDto entity)
		{
			var newTerrain = new Terrain 
			{ 
				description = entity.description,
				name = entity.name,
				userId = entity.userId
			};

			db.Terrains.Add(newTerrain);
			var success = db.SaveChanges() > 0;
			if (success)
				return newTerrain;
			else
				throw new InvalidOperationException("wrong with creating terrain");
		}

		public int UpdateProperty(int entityId, string propertyName, object propertyValue)
		{
			return propertyUpdater.UpdateProperty(entityId, propertyName, propertyValue);
		}

		public void Remove(int entId)
		{
			db.Terrains.Remove(new Terrain { id = entId });
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
