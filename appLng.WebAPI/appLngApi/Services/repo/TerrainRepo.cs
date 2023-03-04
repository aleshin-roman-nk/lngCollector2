using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.repo
{
    public class TerrainRepo
    {
        private readonly DbFactory factory;

        public TerrainRepo(DbFactory factory) 
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
                db.Terrains.Add(t);
                db.SaveChanges();
                return t;
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
