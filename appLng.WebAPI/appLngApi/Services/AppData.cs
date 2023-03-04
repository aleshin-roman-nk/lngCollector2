using Microsoft.EntityFrameworkCore;
using Models;
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
        public DbSet<TestingMissionSession> TestingMissionSessions { get; set; }
        public DbSet<TestingMission> TestingMissions { get; set; }
        public DbSet<LexemMeaning> LexemMeanings { get; set; }
        public DbSet<Lexem> Lexems { get; set; }
        public DbSet<Grade> Grades { get; set; }
    }
}
