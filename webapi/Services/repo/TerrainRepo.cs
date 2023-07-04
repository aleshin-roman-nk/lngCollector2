using Models.Location;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.repo
{
    public class TerrainRepo : ITerrainRepo
    {
        private readonly IDbFactory factory;

        public TerrainRepo(IDbFactory factory)
        {
            this.factory = factory;
        }

        public IEnumerable<Terrain> GetAll()
        {
            using (var db = factory.Create())
            {
                return db.Terrains.ToList();
            }
        }

        public Terrain? Get(int id)
        {
            using (var db = factory.Create())
            {
                return db.Terrains.FirstOrDefault(x => x.id == id);
            }
        }

        public Terrain Create(Terrain t)
        {
            using (var db = factory.Create())
            {
                t.id = 0;

                db.Terrains.Add(t);
                db.SaveChanges();
                return t;
            }
        }

        public Terrain Update(int id, Terrain terr)
        {
            using (var db = factory.Create())
            {
                var t = db.Terrains.FirstOrDefault(x => x.id == id);
                if (t != null)
                {
                    t.name = terr.name;
                    t.description = terr.description;
                    db.SaveChanges();
                    return t;
                }

                throw new InvalidOperationException("Some fault occurred while updating the object.");
            }
        }

        public void Delete(int id)
        {
            using (var db = factory.Create())
            {
                db.Terrains.Remove(new Terrain { id = id });
                db.SaveChanges();
            }
        }
    }
}
