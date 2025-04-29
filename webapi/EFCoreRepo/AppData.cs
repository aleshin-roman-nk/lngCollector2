using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThoughtzLand.Core.Models.Common;
using ThoughtzLand.Core.Models.Location.src;
using ThoughtzLand.EFCoreRepo.Entities;

namespace ThoughtzLand.ImplementRepo.EFCoreRepo
{
	public class AppData: DbContext
	{
		public AppData(DbContextOptions<AppData> options) : base(options) { }
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

			modelBuilder.Entity<FlashCardDb>()
				.HasOne<NodeDb>() // Specifies the related entity type
				.WithMany(node => node.FlashCards) // If FlashCardDb doesn't have a navigation property back to NodeDb, use WithMany without arguments
				.HasForeignKey(f => f.nodeId);// Foreign key property in FlashCardDb

			modelBuilder.Entity<ResearchText>()
				.HasOne<NodeDb>()
				.WithMany(rt => rt.ResearchTexts)
				.HasForeignKey(rt => rt.nodeId);

			modelBuilder.Entity<NodeDb>()
				.HasOne<TerrainDb>()
				.WithMany(terrain => terrain.Nodes)
				.HasForeignKey(node => node.terrainId);
		}

		public DbSet<TerrainDb> Terrains { get; set; }
		public DbSet<NodeDb> Nodes { get; set; }
		public DbSet<Language> Languages { get; set; }
		public DbSet<FlashCardDb> FlashCards { get; set; }
		public DbSet<FlashCardAnswerDb> FlashCardAnswers { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<ResearchText> ResearchTexts { get; set; }
	}
}
