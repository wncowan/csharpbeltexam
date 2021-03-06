﻿// <auto-generated />
using csharpbeltexam.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace csharpbeltexam.Migrations
{
    [DbContext(typeof(BrightIdeaContext))]
    [Migration("20180518175200_SixthMigration")]
    partial class SixthMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125");

            modelBuilder.Entity("csharpbeltexam.Models.BrightIdea", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CreatorId");

                    b.Property<string>("Idea")
                        .IsRequired();

                    b.Property<int?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("BrightIdeas");
                });

            modelBuilder.Entity("csharpbeltexam.Models.Guest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BrightIdeaId");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("BrightIdeaId");

                    b.HasIndex("UserId");

                    b.ToTable("Guests");
                });

            modelBuilder.Entity("csharpbeltexam.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Alias");

                    b.Property<DateTime>("Created_At");

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<string>("Password");

                    b.Property<DateTime>("Updated_At");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("csharpbeltexam.Models.BrightIdea", b =>
                {
                    b.HasOne("csharpbeltexam.Models.User", "User")
                        .WithMany("BrightIdeas")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("csharpbeltexam.Models.Guest", b =>
                {
                    b.HasOne("csharpbeltexam.Models.BrightIdea", "BrightIdea")
                        .WithMany("Guests")
                        .HasForeignKey("BrightIdeaId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("csharpbeltexam.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
