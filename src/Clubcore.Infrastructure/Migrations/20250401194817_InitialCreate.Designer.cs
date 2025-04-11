﻿// <auto-generated />
using System;
using Clubcore.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Clubcore.Infrastructure.Migrations
{
    [DbContext(typeof(ClubcoreDbContext))]
    [Migration("20250401194817_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Clubcore.Domain.AggregatesModel.Club", b =>
                {
                    b.Property<Guid>("ClubId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ClubId");

                    b.ToTable("Clubs");
                });

            modelBuilder.Entity("Clubcore.Domain.AggregatesModel.ClubGroup", b =>
                {
                    b.Property<Guid>("ClubId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("GroupId")
                        .HasColumnType("uuid");

                    b.HasKey("ClubId", "GroupId");

                    b.HasIndex("GroupId");

                    b.ToTable("ClubGroup");
                });

            modelBuilder.Entity("Clubcore.Domain.AggregatesModel.Event", b =>
                {
                    b.Property<Guid>("EventId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("TimeRangeId")
                        .HasColumnType("uuid");

                    b.HasKey("EventId");

                    b.HasIndex("TimeRangeId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("Clubcore.Domain.AggregatesModel.Feedback", b =>
                {
                    b.Property<Guid>("FeedbackId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("EventId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("PersonId")
                        .HasColumnType("uuid");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.HasKey("FeedbackId");

                    b.HasIndex("EventId");

                    b.HasIndex("PersonId");

                    b.ToTable("Feedbacks");
                });

            modelBuilder.Entity("Clubcore.Domain.AggregatesModel.Group", b =>
                {
                    b.Property<Guid>("GroupId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("PersonId")
                        .HasColumnType("uuid");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.HasKey("GroupId");

                    b.HasIndex("PersonId");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("Clubcore.Domain.AggregatesModel.GroupRelationship", b =>
                {
                    b.Property<Guid>("ChildGroupId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ParentGroupId")
                        .HasColumnType("uuid");

                    b.HasKey("ChildGroupId", "ParentGroupId");

                    b.HasIndex("ParentGroupId");

                    b.ToTable("GroupRelationship");
                });

            modelBuilder.Entity("Clubcore.Domain.AggregatesModel.Person", b =>
                {
                    b.Property<Guid>("PersonId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.HasKey("PersonId");

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("Clubcore.Domain.AggregatesModel.PersonRole", b =>
                {
                    b.Property<Guid>("PersonId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("GroupId")
                        .HasColumnType("uuid");

                    b.HasKey("PersonId", "RoleId");

                    b.HasIndex("GroupId");

                    b.HasIndex("RoleId");

                    b.ToTable("PersonRole");
                });

            modelBuilder.Entity("Clubcore.Domain.AggregatesModel.Role", b =>
                {
                    b.Property<Guid>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("RoleId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Clubcore.Domain.AggregatesModel.TimeRange", b =>
                {
                    b.Property<Guid>("TimeRangeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("ClubGroupClubId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("ClubGroupGroupId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("End")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("GroupRelationshipChildGroupId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("GroupRelationshipParentGroupId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("PersonRolePersonId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("PersonRoleRoleId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Start")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("TimeRangeId");

                    b.HasIndex("ClubGroupClubId", "ClubGroupGroupId");

                    b.HasIndex("GroupRelationshipChildGroupId", "GroupRelationshipParentGroupId");

                    b.HasIndex("PersonRolePersonId", "PersonRoleRoleId");

                    b.ToTable("TimeRange");
                });

            modelBuilder.Entity("Clubcore.Domain.AggregatesModel.ClubGroup", b =>
                {
                    b.HasOne("Clubcore.Domain.AggregatesModel.Club", null)
                        .WithMany()
                        .HasForeignKey("ClubId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Clubcore.Domain.AggregatesModel.Group", null)
                        .WithMany("ClubGroups")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Clubcore.Domain.AggregatesModel.Event", b =>
                {
                    b.HasOne("Clubcore.Domain.AggregatesModel.TimeRange", "TimeRange")
                        .WithMany()
                        .HasForeignKey("TimeRangeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TimeRange");
                });

            modelBuilder.Entity("Clubcore.Domain.AggregatesModel.Feedback", b =>
                {
                    b.HasOne("Clubcore.Domain.AggregatesModel.Event", null)
                        .WithMany("Feedbacks")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Clubcore.Domain.AggregatesModel.Person", null)
                        .WithMany("Feedbacks")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Clubcore.Domain.AggregatesModel.Group", b =>
                {
                    b.HasOne("Clubcore.Domain.AggregatesModel.Person", null)
                        .WithMany("Groups")
                        .HasForeignKey("PersonId");
                });

            modelBuilder.Entity("Clubcore.Domain.AggregatesModel.GroupRelationship", b =>
                {
                    b.HasOne("Clubcore.Domain.AggregatesModel.Group", null)
                        .WithMany()
                        .HasForeignKey("ChildGroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Clubcore.Domain.AggregatesModel.Group", null)
                        .WithMany()
                        .HasForeignKey("ParentGroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Clubcore.Domain.AggregatesModel.Person", b =>
                {
                    b.OwnsOne("Clubcore.Domain.AggregatesModel.PersonName", "Name", b1 =>
                        {
                            b1.Property<Guid>("PersonId")
                                .HasColumnType("uuid");

                            b1.Property<string>("FirstName")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<string>("LastName")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<string>("MobileNr")
                                .HasColumnType("text");

                            b1.HasKey("PersonId");

                            b1.ToTable("Persons");

                            b1.WithOwner()
                                .HasForeignKey("PersonId");
                        });

                    b.Navigation("Name")
                        .IsRequired();
                });

            modelBuilder.Entity("Clubcore.Domain.AggregatesModel.PersonRole", b =>
                {
                    b.HasOne("Clubcore.Domain.AggregatesModel.Group", null)
                        .WithMany("PersonRoles")
                        .HasForeignKey("GroupId");

                    b.HasOne("Clubcore.Domain.AggregatesModel.Person", null)
                        .WithMany()
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Clubcore.Domain.AggregatesModel.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Clubcore.Domain.AggregatesModel.TimeRange", b =>
                {
                    b.HasOne("Clubcore.Domain.AggregatesModel.ClubGroup", null)
                        .WithMany("TimeRanges")
                        .HasForeignKey("ClubGroupClubId", "ClubGroupGroupId");

                    b.HasOne("Clubcore.Domain.AggregatesModel.GroupRelationship", null)
                        .WithMany("TimeRanges")
                        .HasForeignKey("GroupRelationshipChildGroupId", "GroupRelationshipParentGroupId");

                    b.HasOne("Clubcore.Domain.AggregatesModel.PersonRole", null)
                        .WithMany("TimeRanges")
                        .HasForeignKey("PersonRolePersonId", "PersonRoleRoleId");
                });

            modelBuilder.Entity("Clubcore.Domain.AggregatesModel.ClubGroup", b =>
                {
                    b.Navigation("TimeRanges");
                });

            modelBuilder.Entity("Clubcore.Domain.AggregatesModel.Event", b =>
                {
                    b.Navigation("Feedbacks");
                });

            modelBuilder.Entity("Clubcore.Domain.AggregatesModel.Group", b =>
                {
                    b.Navigation("ClubGroups");

                    b.Navigation("PersonRoles");
                });

            modelBuilder.Entity("Clubcore.Domain.AggregatesModel.GroupRelationship", b =>
                {
                    b.Navigation("TimeRanges");
                });

            modelBuilder.Entity("Clubcore.Domain.AggregatesModel.Person", b =>
                {
                    b.Navigation("Feedbacks");

                    b.Navigation("Groups");
                });

            modelBuilder.Entity("Clubcore.Domain.AggregatesModel.PersonRole", b =>
                {
                    b.Navigation("TimeRanges");
                });
#pragma warning restore 612, 618
        }
    }
}
