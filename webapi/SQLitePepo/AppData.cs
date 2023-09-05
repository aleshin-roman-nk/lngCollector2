using Microsoft.EntityFrameworkCore;
using ThoughtzLand.Core.Models.Common;
using ThoughtzLand.Core.Models.Exam;
using ThoughtzLand.Core.Models.Location;
using ThoughtzLand.Core.Models.Thoughts;
using ThoughtzLand.ImplementRepo.SQLitePepo.Entities;

namespace ThoughtzLand.ImplementRepo.SQLitePepo
{
    public class AppData: DbContext
    {
        public AppData(DbContextOptions<AppData> options): base(options) { }

        public DbSet<Terrain> Terrains { get; set; }
        public DbSet<Node> Nodes { get; set; }
        public DbSet<Thought> Thoughts { get; set; }
        public DbSet<ThExpression> ThExpressions { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<FlashCardDb> FlashCards { get; set; }
    }
}
