﻿// <auto-generated />
using System;
using DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataAccess.Migrations
{
    [DbContext(typeof(SimplifyAppDbContext))]
    [Migration("20241108173743_Sprint table changes")]
    partial class Sprinttablechanges
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DataAccess.Model.ConnectionTables.MeetingAttachments", b =>
                {
                    b.Property<string>("MeetingGUID")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<int>("AttachmentID")
                        .HasMaxLength(36)
                        .HasColumnType("int");

                    b.HasKey("MeetingGUID", "AttachmentID");

                    SqlServerKeyBuilderExtensions.IsClustered(b.HasKey("MeetingGUID", "AttachmentID"), false);

                    b.HasIndex("AttachmentID");

                    b.ToTable("MeetingAttachments");
                });

            modelBuilder.Entity("DataAccess.Model.ConnectionTables.TeammateMeetings", b =>
                {
                    b.Property<string>("TeammateGUID")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("MeetingGUID")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.HasKey("TeammateGUID", "MeetingGUID");

                    SqlServerKeyBuilderExtensions.IsClustered(b.HasKey("TeammateGUID", "MeetingGUID"), false);

                    b.HasIndex("MeetingGUID");

                    b.ToTable("TeammateMeetings");
                });

            modelBuilder.Entity("DataAccess.Model.Meetings.Attachment", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(100000)
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("Attachments");
                });

            modelBuilder.Entity("DataAccess.Model.Meetings.Meeting", b =>
                {
                    b.Property<string>("GUID")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<string>("Description")
                        .HasMaxLength(10000)
                        .HasColumnType("nvarchar(max)");

                    b.Property<TimeSpan>("Duration")
                        .HasColumnType("time");

                    b.Property<string>("MeetingLeaderGUID")
                        .IsRequired()
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<DateTime>("Start")
                        .HasColumnType("datetime2");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("GUID");

                    b.HasIndex("MeetingLeaderGUID");

                    b.ToTable("Meetings");
                });

            modelBuilder.Entity("DataAccess.Model.User.Team", b =>
                {
                    b.Property<string>("GUID")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<string>("ManagerGUID")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("GUID");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("DataAccess.Model.User.Teammate", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Nickname")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<int?>("ScrumRole")
                        .HasColumnType("int");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TeamGUID")
                        .HasColumnType("nvarchar(36)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.HasIndex("TeamGUID");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("DataAccess.Models.Projects.Feature", b =>
                {
                    b.Property<string>("GUID")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<string>("Creator")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(10000)
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastUpdate")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int?>("Points")
                        .HasColumnType("int");

                    b.Property<string>("ProjectGUID")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<int>("State")
                        .HasColumnType("int");

                    b.HasKey("GUID");

                    b.HasIndex("Creator");

                    b.HasIndex("LastUpdate");

                    b.HasIndex("ProjectGUID");

                    b.ToTable("Features", t =>
                        {
                            t.HasCheckConstraint("CK_Features_Points", "[Points] IN (1, 2, 3, 5, 8, 13)");
                        });
                });

            modelBuilder.Entity("DataAccess.Models.Projects.Project", b =>
                {
                    b.Property<string>("GUID")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<string>("Creator")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Description")
                        .HasMaxLength(10000)
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastUpdate")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("State")
                        .HasColumnType("int");

                    b.Property<string>("TeamGUID")
                        .IsRequired()
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.HasKey("GUID");

                    b.HasIndex("Creator");

                    b.HasIndex("LastUpdate");

                    b.HasIndex("TeamGUID");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("DataAccess.Models.Projects.Sprint", b =>
                {
                    b.Property<string>("GUID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Creator")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("End")
                        .HasColumnType("datetime2");

                    b.Property<string>("Goal")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<int>("Iteration")
                        .HasColumnType("int");

                    b.Property<string>("LastUpdate")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("ProjectGUID")
                        .IsRequired()
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.HasKey("GUID");

                    b.HasIndex("Creator");

                    b.HasIndex("LastUpdate");

                    b.HasIndex("ProjectGUID");

                    b.ToTable("Sprints");
                });

            modelBuilder.Entity("DataAccess.Models.Projects.SprintNote", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("SprintID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("TeammateGUID")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ID");

                    b.HasIndex("SprintID");

                    b.HasIndex("TeammateGUID");

                    b.ToTable("SprintNotes");
                });

            modelBuilder.Entity("DataAccess.Models.Projects.Task", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Assigne")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Creator")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FeatureGUID")
                        .IsRequired()
                        .HasColumnType("nvarchar(36)");

                    b.Property<string>("LastUpdate")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<int>("State")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("Assigne");

                    b.HasIndex("Creator");

                    b.HasIndex("FeatureGUID");

                    b.HasIndex("LastUpdate");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("DataAccess.Models.Tracking.ActionHistory", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("ID"));

                    b.Property<int>("Action")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("Item")
                        .HasColumnType("int");

                    b.Property<string>("ItemGUID")
                        .IsRequired()
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<string>("UserGUID")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ID");

                    b.HasIndex("UserGUID");

                    b.ToTable("ActionHistories");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("DataAccess.Model.ConnectionTables.MeetingAttachments", b =>
                {
                    b.HasOne("DataAccess.Model.Meetings.Attachment", "Attachment")
                        .WithMany("MeetingAttachments")
                        .HasForeignKey("AttachmentID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("DataAccess.Model.Meetings.Meeting", "Meeting")
                        .WithMany("MeetingAttachments")
                        .HasForeignKey("MeetingGUID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Attachment");

                    b.Navigation("Meeting");
                });

            modelBuilder.Entity("DataAccess.Model.ConnectionTables.TeammateMeetings", b =>
                {
                    b.HasOne("DataAccess.Model.Meetings.Meeting", "Meeting")
                        .WithMany("TeammateMeetings")
                        .HasForeignKey("MeetingGUID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("DataAccess.Model.User.Teammate", "Teammate")
                        .WithMany("TeammateMeetings")
                        .HasForeignKey("TeammateGUID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Meeting");

                    b.Navigation("Teammate");
                });

            modelBuilder.Entity("DataAccess.Model.Meetings.Meeting", b =>
                {
                    b.HasOne("DataAccess.Model.User.Teammate", "MeetingLeader")
                        .WithMany()
                        .HasForeignKey("MeetingLeaderGUID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MeetingLeader");
                });

            modelBuilder.Entity("DataAccess.Model.User.Teammate", b =>
                {
                    b.HasOne("DataAccess.Model.User.Team", "Team")
                        .WithMany("TeamMembers")
                        .HasForeignKey("TeamGUID");

                    b.Navigation("Team");
                });

            modelBuilder.Entity("DataAccess.Models.Projects.Feature", b =>
                {
                    b.HasOne("DataAccess.Model.User.Teammate", null)
                        .WithMany()
                        .HasForeignKey("Creator")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("DataAccess.Model.User.Teammate", null)
                        .WithMany()
                        .HasForeignKey("LastUpdate")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("DataAccess.Models.Projects.Project", "ParentProject")
                        .WithMany("Features")
                        .HasForeignKey("ProjectGUID");

                    b.Navigation("ParentProject");
                });

            modelBuilder.Entity("DataAccess.Models.Projects.Project", b =>
                {
                    b.HasOne("DataAccess.Model.User.Teammate", null)
                        .WithMany()
                        .HasForeignKey("Creator")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("DataAccess.Model.User.Teammate", null)
                        .WithMany()
                        .HasForeignKey("LastUpdate")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("DataAccess.Model.User.Team", "ProjectTeam")
                        .WithMany("Projects")
                        .HasForeignKey("TeamGUID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("ProjectTeam");
                });

            modelBuilder.Entity("DataAccess.Models.Projects.Sprint", b =>
                {
                    b.HasOne("DataAccess.Model.User.Teammate", null)
                        .WithMany()
                        .HasForeignKey("Creator")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("DataAccess.Model.User.Teammate", null)
                        .WithMany()
                        .HasForeignKey("LastUpdate")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("DataAccess.Models.Projects.Project", "Project")
                        .WithMany("Sprints")
                        .HasForeignKey("ProjectGUID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");
                });

            modelBuilder.Entity("DataAccess.Models.Projects.SprintNote", b =>
                {
                    b.HasOne("DataAccess.Models.Projects.Sprint", "Sprint")
                        .WithMany("SprintNotes")
                        .HasForeignKey("SprintID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataAccess.Model.User.Teammate", "Teammate")
                        .WithMany("SprintNotes")
                        .HasForeignKey("TeammateGUID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("DataAccess.Models.Projects.SprintNoteValue", "Value", b1 =>
                        {
                            b1.Property<int>("SprintNoteID")
                                .HasColumnType("int");

                            b1.HasKey("SprintNoteID");

                            b1.ToTable("SprintNotes");

                            b1.ToJson("Value");

                            b1.WithOwner()
                                .HasForeignKey("SprintNoteID");
                        });

                    b.Navigation("Sprint");

                    b.Navigation("Teammate");

                    b.Navigation("Value")
                        .IsRequired();
                });

            modelBuilder.Entity("DataAccess.Models.Projects.Task", b =>
                {
                    b.HasOne("DataAccess.Model.User.Teammate", "AssignedTeammate")
                        .WithMany("Tasks")
                        .HasForeignKey("Assigne");

                    b.HasOne("DataAccess.Model.User.Teammate", null)
                        .WithMany()
                        .HasForeignKey("Creator")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataAccess.Models.Projects.Feature", "ParentFeature")
                        .WithMany("Tasks")
                        .HasForeignKey("FeatureGUID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataAccess.Model.User.Teammate", null)
                        .WithMany()
                        .HasForeignKey("LastUpdate")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("AssignedTeammate");

                    b.Navigation("ParentFeature");
                });

            modelBuilder.Entity("DataAccess.Models.Tracking.ActionHistory", b =>
                {
                    b.HasOne("DataAccess.Model.User.Teammate", null)
                        .WithMany()
                        .HasForeignKey("UserGUID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("DataAccess.Model.User.Teammate", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("DataAccess.Model.User.Teammate", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataAccess.Model.User.Teammate", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("DataAccess.Model.User.Teammate", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DataAccess.Model.Meetings.Attachment", b =>
                {
                    b.Navigation("MeetingAttachments");
                });

            modelBuilder.Entity("DataAccess.Model.Meetings.Meeting", b =>
                {
                    b.Navigation("MeetingAttachments");

                    b.Navigation("TeammateMeetings");
                });

            modelBuilder.Entity("DataAccess.Model.User.Team", b =>
                {
                    b.Navigation("Projects");

                    b.Navigation("TeamMembers");
                });

            modelBuilder.Entity("DataAccess.Model.User.Teammate", b =>
                {
                    b.Navigation("SprintNotes");

                    b.Navigation("Tasks");

                    b.Navigation("TeammateMeetings");
                });

            modelBuilder.Entity("DataAccess.Models.Projects.Feature", b =>
                {
                    b.Navigation("Tasks");
                });

            modelBuilder.Entity("DataAccess.Models.Projects.Project", b =>
                {
                    b.Navigation("Features");

                    b.Navigation("Sprints");
                });

            modelBuilder.Entity("DataAccess.Models.Projects.Sprint", b =>
                {
                    b.Navigation("SprintNotes");
                });
#pragma warning restore 612, 618
        }
    }
}
