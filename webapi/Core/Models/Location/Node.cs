using ThoughtzLand.Core.Models.Common;

namespace ThoughtzLand.Core.Models.Location
{
    public class Node: IDbEntity
    {
        public int id { get; set; }
        public int terrainId { get; set; }
        public string? name { get; set; }
        public string? description { get; set; }
        public int x { get; set; }// при создании, на сервере спрашиваем у территории последние доступное место
        public int y { get; set; }
    }
}
