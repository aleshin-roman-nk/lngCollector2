using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using ThoughtzLand.Core.Models.Common;
using ThoughtzLand.Core.Models.Exam;
using ThoughtzLand.Core.Models.Location;
using ThoughtzLand.Core.Models.Location.src;
using ThoughtzLand.ImplementRepo.SQLitePepo.Entities;

namespace ThoughtzLand.ImplementRepo.SQLitePepo
{
    public class AppData: DbContext
    {
        public AppData(DbContextOptions<AppData> options): base(options) { }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
			//optionsBuilder.EnableSensitiveDataLogging();
			//optionsBuilder.LogTo(x => Console.WriteLine(x));
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			// Configuration for FlashCardAnswerDb entity
			modelBuilder.Entity<FlashCardAnswerDb>()
				.HasOne<FlashCardDb>() // Specifies the related entity type
				.WithMany(f => f.answers) // Specifies the collection navigation property in the FlashCard entity
				.HasForeignKey(a => a.cardId); // Specifies the foreign key in the FlashCardAnswerDb entity

			modelBuilder.Entity<FlashCardDb>()
				.HasOne(f => f.language) // Navigation property in FlashCardDb
				.WithMany() // If ThExpression doesn't have a navigation property back to FlashCardDb, use WithMany without arguments
				.HasForeignKey(f => f.languageId);// Foreign key property in FlashCardDb
		}

		public DbSet<Terrain> Terrains { get; set; }
        public DbSet<Node> Nodes { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<FlashCardDb> FlashCards { get; set; }
        public DbSet<FlashCardAnswerDb> FlashCardAnswers { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<ResearchText> ResearchTexts { get; set; }

	}
}
