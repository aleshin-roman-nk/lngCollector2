using ThoughtzLand.Core.Models.Common;

namespace ThoughtzLand.Core.Models.Location
{
    public class Terrain: IDbEntity
    {
        public int id { get; set; }
        public string? name { get; set; }
        public string? description { get; set; }
    }
}
