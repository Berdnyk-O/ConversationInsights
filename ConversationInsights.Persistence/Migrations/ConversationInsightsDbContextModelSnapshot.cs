﻿// <auto-generated />
using System;
using ConversationInsights.Persistence.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ConversationInsights.Persistence.Migrations
{
    [DbContext(typeof(ConversationInsightsDbContext))]
    partial class ConversationInsightsDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ConversationInsights.Domain.Entities.Call", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("EmotionalTone")
                        .HasColumnType("integer");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Calls");
                });

            modelBuilder.Entity("ConversationInsights.Domain.Entities.CallCategory", b =>
                {
                    b.Property<Guid>("CallId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uuid");

                    b.HasKey("CallId", "CategoryId");

                    b.HasIndex("CategoryId");

                    b.ToTable("CallCategories");
                });

            modelBuilder.Entity("ConversationInsights.Domain.Entities.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string[]>("Points")
                        .IsRequired()
                        .HasColumnType("text[]");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("ConversationInsights.Domain.Entities.CallCategory", b =>
                {
                    b.HasOne("ConversationInsights.Domain.Entities.Call", "Call")
                        .WithMany("CallCategories")
                        .HasForeignKey("CallId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ConversationInsights.Domain.Entities.Category", "Category")
                        .WithMany("CallCategories")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Call");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("ConversationInsights.Domain.Entities.Call", b =>
                {
                    b.Navigation("CallCategories");
                });

            modelBuilder.Entity("ConversationInsights.Domain.Entities.Category", b =>
                {
                    b.Navigation("CallCategories");
                });
#pragma warning restore 612, 618
        }
    }
}
