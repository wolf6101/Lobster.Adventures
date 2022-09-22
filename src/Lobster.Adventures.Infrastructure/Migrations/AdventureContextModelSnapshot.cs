﻿// <auto-generated />
using System;
using Lobster.Adventures.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Lobster.Adventures.Infrastructure.Migrations
{
    [DbContext(typeof(AdventureContext))]
    partial class AdventureContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Lobster.Adventures.Domain.Entities.Adventure", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumberOfNodes")
                        .HasColumnType("int");

                    b.Property<Guid>("RootNodeId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Adventures", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("35168b83-b5f4-4079-b674-12b5f32e995e"),
                            Description = "Adventure",
                            Name = "Doughnut adventure",
                            NumberOfNodes = 9,
                            RootNodeId = new Guid("209005df-5897-4491-992e-c25cd9aca290")
                        });
                });

            modelBuilder.Entity("Lobster.Adventures.Domain.Entities.AdventureNode", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AdventureId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("LeftChildId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ParentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("RightChildId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("AdventureId");

                    b.ToTable("AdventureNode", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("209005df-5897-4491-992e-c25cd9aca290"),
                            AdventureId = new Guid("35168b83-b5f4-4079-b674-12b5f32e995e"),
                            LeftChildId = new Guid("0e4a446b-adc7-430d-8b84-5ffaca507682"),
                            Name = "Do I want Doughnut?",
                            RightChildId = new Guid("f287a87e-148c-4ffe-aed7-37bb6baefbb8")
                        },
                        new
                        {
                            Id = new Guid("0e4a446b-adc7-430d-8b84-5ffaca507682"),
                            AdventureId = new Guid("35168b83-b5f4-4079-b674-12b5f32e995e"),
                            LeftChildId = new Guid("f7aa73d6-5566-4278-8ae7-a8e273d944a8"),
                            Name = "Do I deserve it?",
                            ParentId = new Guid("209005df-5897-4491-992e-c25cd9aca290"),
                            RightChildId = new Guid("130454c8-4a63-40b3-b400-b2b13dc34809")
                        },
                        new
                        {
                            Id = new Guid("f287a87e-148c-4ffe-aed7-37bb6baefbb8"),
                            AdventureId = new Guid("35168b83-b5f4-4079-b674-12b5f32e995e"),
                            Name = "Maybe you want an apple?",
                            ParentId = new Guid("209005df-5897-4491-992e-c25cd9aca290")
                        },
                        new
                        {
                            Id = new Guid("f7aa73d6-5566-4278-8ae7-a8e273d944a8"),
                            AdventureId = new Guid("35168b83-b5f4-4079-b674-12b5f32e995e"),
                            LeftChildId = new Guid("096a086d-980b-44cb-bfa3-382d8f844ee2"),
                            Name = "Are you sure?",
                            ParentId = new Guid("0e4a446b-adc7-430d-8b84-5ffaca507682"),
                            RightChildId = new Guid("95069404-d73c-4ba9-8a1e-5f76bb51e790")
                        },
                        new
                        {
                            Id = new Guid("130454c8-4a63-40b3-b400-b2b13dc34809"),
                            AdventureId = new Guid("35168b83-b5f4-4079-b674-12b5f32e995e"),
                            LeftChildId = new Guid("4ae2afff-92e8-4ac3-b934-3d07be023f3d"),
                            Name = "Is it a good doughnut?",
                            ParentId = new Guid("0e4a446b-adc7-430d-8b84-5ffaca507682"),
                            RightChildId = new Guid("2f6a6663-90f8-4313-8441-dda39df5d677")
                        },
                        new
                        {
                            Id = new Guid("096a086d-980b-44cb-bfa3-382d8f844ee2"),
                            AdventureId = new Guid("35168b83-b5f4-4079-b674-12b5f32e995e"),
                            Name = "Get it!",
                            ParentId = new Guid("f7aa73d6-5566-4278-8ae7-a8e273d944a8")
                        },
                        new
                        {
                            Id = new Guid("95069404-d73c-4ba9-8a1e-5f76bb51e790"),
                            AdventureId = new Guid("35168b83-b5f4-4079-b674-12b5f32e995e"),
                            Name = "Do jumping jacks first!",
                            ParentId = new Guid("f7aa73d6-5566-4278-8ae7-a8e273d944a8")
                        },
                        new
                        {
                            Id = new Guid("4ae2afff-92e8-4ac3-b934-3d07be023f3d"),
                            AdventureId = new Guid("35168b83-b5f4-4079-b674-12b5f32e995e"),
                            Name = "Grab it now!",
                            ParentId = new Guid("130454c8-4a63-40b3-b400-b2b13dc34809")
                        },
                        new
                        {
                            Id = new Guid("2f6a6663-90f8-4313-8441-dda39df5d677"),
                            AdventureId = new Guid("35168b83-b5f4-4079-b674-12b5f32e995e"),
                            Name = "Wait for a better one!",
                            ParentId = new Guid("130454c8-4a63-40b3-b400-b2b13dc34809")
                        });
                });

            modelBuilder.Entity("Lobster.Adventures.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("4d281e58-9789-4def-ad47-f2f2f98df30e"),
                            CreatedDateTime = new DateTime(2022, 9, 21, 23, 35, 21, 164, DateTimeKind.Local).AddTicks(4936),
                            Email = "johnsmith@email.com",
                            FirstName = "John",
                            LastName = "Smith"
                        });
                });

            modelBuilder.Entity("Lobster.Adventures.Domain.Entities.UserJourney", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AdventureId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateTimeCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateTimeUpdated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Path")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("AdventureId");

                    b.HasIndex("UserId");

                    b.ToTable("UserJourneys", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("254d8f7a-bc27-4aff-bb4d-1b232b8de4a6"),
                            AdventureId = new Guid("35168b83-b5f4-4079-b674-12b5f32e995e"),
                            DateTimeCreated = new DateTime(2022, 9, 21, 22, 35, 21, 164, DateTimeKind.Utc).AddTicks(5258),
                            DateTimeUpdated = new DateTime(2022, 9, 21, 22, 35, 21, 164, DateTimeKind.Utc).AddTicks(5259),
                            Path = ",209005df-5897-4491-992e-c25cd9aca290,0e4a446b-adc7-430d-8b84-5ffaca507682,f7aa73d6-5566-4278-8ae7-a8e273d944a8,096a086d-980b-44cb-bfa3-382d8f844ee2,",
                            Status = 0,
                            UserId = new Guid("4d281e58-9789-4def-ad47-f2f2f98df30e")
                        });
                });

            modelBuilder.Entity("Lobster.Adventures.Domain.Entities.AdventureNode", b =>
                {
                    b.HasOne("Lobster.Adventures.Domain.Entities.Adventure", "Adventure")
                        .WithMany("Nodes")
                        .HasForeignKey("AdventureId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("Adventure");
                });

            modelBuilder.Entity("Lobster.Adventures.Domain.Entities.UserJourney", b =>
                {
                    b.HasOne("Lobster.Adventures.Domain.Entities.Adventure", "Adventure")
                        .WithMany("Journeys")
                        .HasForeignKey("AdventureId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Lobster.Adventures.Domain.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Adventure");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Lobster.Adventures.Domain.Entities.Adventure", b =>
                {
                    b.Navigation("Journeys");

                    b.Navigation("Nodes");
                });
#pragma warning restore 612, 618
        }
    }
}
