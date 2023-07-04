using Microsoft.EntityFrameworkCore;
using Models.Exam;
using Models.Location;
using Models.Thought;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AppData: DbContext
    {
        string _path;

        public AppData(string path): base()
        {
            _path = path;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename={_path}");

            //optionsBuilder.LogTo(Console.WriteLine);
            //optionsBuilder.EnableSensitiveDataLogging(true);
        }

        public DbSet<Terrain> Terrains { get; set; }
        public DbSet<Node> Nodes { get; set; }
        public DbSet<Thought> Thoughts { get; set; }
        public DbSet<ThExpression> ThExpressions { get; set; }
        public DbSet<Grade> Grades { get; set; }
    }
}
