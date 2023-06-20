using Models.Location;

namespace Services.repo
{
    public interface ITerrainRepo
    {
        Terrain Create(Terrain t);
        void Delete(int id);
        Terrain? Get(int id);
        IEnumerable<Terrain> GetAll();
        Terrain Update(int id, Terrain terr);
    }
}