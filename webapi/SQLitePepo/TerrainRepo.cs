using ThoughtzLand.Core.Models.Location;
using ThoughtzLand.Core.Repos;
using ThoughtzLand.Core.Repos.Common;

namespace ThoughtzLand.ImplementRepo.SQLitePepo
{
    public class TerrainRepo : ITerrainRepo
    {
        private readonly AppData db;

        public TerrainRepo(AppData db)
        {
            this.db = db;
        }

        public IEnumerable<Terrain> GetAll()
        {
            return db.Terrains.ToList();
        }

        public Terrain GetById(int id)
        {
            var res = db.Terrains.FirstOrDefault(x => x.id == id);

            if (res != null)
                return res;
            else
                throw new InvalidOperationException($"no such terrain with id = {id}");
        }

        public Terrain Create(Terrain entity)
        {
            entity.id = 0;

            db.Terrains.Add(entity);
            var success = db.SaveChanges() > 0;
            if (success)
                return entity;
            else
                throw new InvalidOperationException("wrong with creating terrain");
        }

        public Terrain Update(Terrain entity)
        {
            var res = db.Terrains.FirstOrDefault(x => x.id == entity.id);
            if (res == null)
                throw new InvalidOperationException($"wrong with updating terrain id = {entity.id}");

            res.name = entity.name;
            res.description = entity.description;
            var success = db.SaveChanges() > 0;
            if (success)
                return res;
            else
                throw new InvalidOperationException($"wrong with updating terrain id = {entity.id}");
        }

        public void Remove(int entId)
        {
            db.Terrains.Remove(new Terrain { id = entId });
            var success = db.SaveChanges() > 0;

            if (!success)
                throw new InvalidOperationException($"wrong with deleting terrain id = {entId}");
        }
    }
}
