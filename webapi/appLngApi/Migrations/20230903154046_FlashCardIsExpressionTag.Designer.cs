﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ThoughtzLand.ImplementRepo.SQLitePepo;

#nullable disable

namespace ThoughtzLand.Api.Migrations
{
    [DbContext(typeof(AppData))]
    [Migration("20230903154046_FlashCardIsExpressionTag")]
    partial class FlashCardIsExpressionTag
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.10");

            modelBuilder.Entity("ThoughtzLand.Core.Models.Common.Language", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("tag")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("id");

                    b.ToTable("Languages");
                });

            modelBuilder.Entity("ThoughtzLand.Core.Models.Exam.Grade", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<int>("nodeId")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("value")
                        .HasColumnType("TEXT");

                    b.HasKey("id");

                    b.ToTable("Grades");
                });

            modelBuilder.Entity("ThoughtzLand.Core.Models.Location.Node", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("description")
                        .HasColumnType("TEXT");

                    b.Property<string>("name")
                        .HasColumnType("TEXT");

                    b.Property<int>("terrainId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("x")
                        .HasColumnType("INTEGER");

                    b.Property<int>("y")
                        .HasColumnType("INTEGER");

                    b.HasKey("id");

                    b.ToTable("Nodes");
                });

            modelBuilder.Entity("ThoughtzLand.Core.Models.Location.Terrain", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("description")
                        .HasColumnType("TEXT");

                    b.Property<string>("name")
                        .HasColumnType("TEXT");

                    b.HasKey("id");

                    b.ToTable("Terrains");
                });

            modelBuilder.Entity("ThoughtzLand.Core.Models.Thoughts.ThExpression", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("createdDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("lngId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("madeBy")
                        .HasColumnType("INTEGER");

                    b.Property<string>("text")
                        .HasColumnType("TEXT");

                    b.Property<int>("thoughtId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("type")
                        .HasColumnType("INTEGER");

                    b.HasKey("id");

                    b.HasIndex("thoughtId");

                    b.ToTable("ThExpressions");
                });

            modelBuilder.Entity("ThoughtzLand.Core.Models.Thoughts.Thought", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("createdDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("description")
                        .HasColumnType("TEXT");

                    b.Property<int>("nodeId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("text")
                        .HasColumnType("TEXT");

                    b.HasKey("id");

                    b.ToTable("Thoughts");
                });

            modelBuilder.Entity("ThoughtzLand.ImplementRepo.SQLitePepo.Entities.FlashCardDb", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("LastExam")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("NextExamDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("SpacedRepetitionBoxCellId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("expressionId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("nodeId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("rightSolutionScores")
                        .HasColumnType("INTEGER");

                    b.HasKey("id");

                    b.HasIndex("expressionId");

                    b.ToTable("FlashCards");
                });

            modelBuilder.Entity("ThoughtzLand.Core.Models.Thoughts.ThExpression", b =>
                {
                    b.HasOne("ThoughtzLand.Core.Models.Thoughts.Thought", null)
                        .WithMany("expressions")
                        .HasForeignKey("thoughtId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ThoughtzLand.ImplementRepo.SQLitePepo.Entities.FlashCardDb", b =>
                {
                    b.HasOne("ThoughtzLand.Core.Models.Thoughts.ThExpression", "expression")
                        .WithMany()
                        .HasForeignKey("expressionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("expression");
                });

            modelBuilder.Entity("ThoughtzLand.Core.Models.Thoughts.Thought", b =>
                {
                    b.Navigation("expressions");
                });
#pragma warning restore 612, 618
        }
    }
}
