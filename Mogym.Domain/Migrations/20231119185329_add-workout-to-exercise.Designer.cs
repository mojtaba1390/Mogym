﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Mogym.Domain.Context;

#nullable disable

namespace Mogym.Domain.Migrations
{
    [DbContext(typeof(MogymContext))]
    [Migration("20231119185329_add-workout-to-exercise")]
    partial class addworkouttoexercise
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Mogym.Domain.Entities.Exercise", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ExerciseVideoId")
                        .HasColumnType("int");

                    b.Property<DateTime>("InsertDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.Property<int?>("SuperSetId")
                        .HasColumnType("int");

                    b.Property<int>("WorkoutId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ExerciseVideoId")
                        .IsUnique();

                    b.HasIndex("SuperSetId");

                    b.HasIndex("WorkoutId");

                    b.ToTable("Exercise", (string)null);
                });

            modelBuilder.Entity("Mogym.Domain.Entities.ExerciseSet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("Count")
                        .HasColumnType("int");

                    b.Property<int>("ExerciseId")
                        .HasColumnType("int");

                    b.Property<DateTime>("InsertDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("Minute")
                        .HasColumnType("int");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.Property<int?>("Second")
                        .HasColumnType("int");

                    b.Property<int?>("Weight")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ExerciseId");

                    b.ToTable("ExerciseSet", (string)null);
                });

            modelBuilder.Entity("Mogym.Domain.Entities.ExerciseVideo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("FileName")
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("InsertDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("ExerciseVideo", (string)null);
                });

            modelBuilder.Entity("Mogym.Domain.Entities.Log.SeriLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Exception")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Level")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)");

                    b.Property<string>("LogEvent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MessageTemplate")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Properties")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("TimeStamp")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime");

                    b.HasKey("Id");

                    b.ToTable("SeriLog", (string)null);
                });

            modelBuilder.Entity("Mogym.Domain.Entities.Log.UserLogging", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("InsertDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2");

                    b.Property<string>("Ip")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Permalink")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("UserLogging", (string)null);
                });

            modelBuilder.Entity("Mogym.Domain.Entities.Menu", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("EnglishName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar");

                    b.Property<int?>("HasParentInPermission")
                        .HasColumnType("int");

                    b.Property<DateTime>("InsertDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("IsActive")
                        .HasColumnType("int");

                    b.Property<DateTime?>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Link")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar");

                    b.Property<int?>("ParentId")
                        .HasColumnType("int");

                    b.Property<string>("PersianName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("Menu", (string)null);
                });

            modelBuilder.Entity("Mogym.Domain.Entities.Permission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("EnglishName")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("varchar");

                    b.Property<DateTime>("InsertDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.Property<DateTime?>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ParentId")
                        .HasColumnType("int");

                    b.Property<string>("PersianName")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("Permission", (string)null);
                });

            modelBuilder.Entity("Mogym.Domain.Entities.Plan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AnserQuestionId")
                        .HasColumnType("int");

                    b.Property<DateTime>("InsertDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PaidPicture")
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("PlanStatus")
                        .HasColumnType("int");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.Property<int>("TrainerId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AnserQuestionId");

                    b.HasIndex("TrainerId");

                    b.HasIndex("UserId");

                    b.ToTable("Plan", (string)null);
                });

            modelBuilder.Entity("Mogym.Domain.Entities.Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("BackPic")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar");

                    b.Property<double?>("Biceps")
                        .HasColumnType("float");

                    b.Property<double?>("Chest")
                        .HasColumnType("float");

                    b.Property<int?>("DailyAvtivity")
                        .HasColumnType("int");

                    b.Property<int?>("DiabetesAsthmaHypoglycemia")
                        .HasColumnType("int");

                    b.Property<string>("Disease")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar");

                    b.Property<string>("Expection")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar");

                    b.Property<string>("FirstName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar");

                    b.Property<double?>("Fist")
                        .HasColumnType("float");

                    b.Property<string>("FrontPic")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<int?>("HeartDisease")
                        .HasColumnType("int");

                    b.Property<int>("Height")
                        .HasColumnType("int");

                    b.Property<string>("Injury")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar");

                    b.Property<DateTime>("InsertDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastName")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar");

                    b.Property<string>("LeftPic")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar");

                    b.Property<string>("Medicine")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar");

                    b.Property<int?>("NightWork")
                        .HasColumnType("int");

                    b.Property<string>("OutOfGymActivity")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar");

                    b.Property<string>("RightPic")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.Property<int?>("SessionsInWeek")
                        .HasColumnType("int");

                    b.Property<int?>("Smoke")
                        .HasColumnType("int");

                    b.Property<double?>("Thigh")
                        .HasColumnType("float");

                    b.Property<int>("TrainerPlan")
                        .HasColumnType("int");

                    b.Property<string>("Treated")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar");

                    b.Property<double?>("Waist")
                        .HasColumnType("float");

                    b.Property<double>("Weight")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Question", (string)null);
                });

            modelBuilder.Entity("Mogym.Domain.Entities.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("EnglishName")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("varchar");

                    b.Property<DateTime>("InsertDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.Property<DateTime?>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PersianName")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.HasKey("Id");

                    b.ToTable("Role", (string)null);
                });

            modelBuilder.Entity("Mogym.Domain.Entities.RolePermission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("InsertDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("PermissionId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.HasKey("Id");

                    b.HasIndex("PermissionId");

                    b.HasIndex("RoleId");

                    b.ToTable("RolePermission", (string)null);
                });

            modelBuilder.Entity("Mogym.Domain.Entities.TrainerAchievement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("Date")
                        .HasColumnType("int");

                    b.Property<DateTime>("InsertDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("TrainerProfileId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TrainerProfileId");

                    b.ToTable("TrainerAchievement", (string)null);
                });

            modelBuilder.Entity("Mogym.Domain.Entities.TrainerPlanCost", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("InsertDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<double?>("OriginalCost")
                        .HasColumnType("float");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.Property<double?>("SaleCost")
                        .HasColumnType("float");

                    b.Property<int>("TrainerPlan")
                        .HasColumnType("int");

                    b.Property<int>("TrainerProfileId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TrainerProfileId");

                    b.ToTable("TrainerPlanCost", (string)null);
                });

            modelBuilder.Entity("Mogym.Domain.Entities.TrainerProfile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Biography")
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("CartNumber")
                        .HasColumnType("char(19)");

                    b.Property<string>("CartOwnerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime>("InsertDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("TrainerProfile", (string)null);
                });

            modelBuilder.Entity("Mogym.Domain.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("BirthDay")
                        .HasMaxLength(10)
                        .HasColumnType("varchar");

                    b.Property<string>("Email")
                        .HasMaxLength(100)
                        .HasColumnType("varchar");

                    b.Property<string>("FirstName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar");

                    b.Property<int?>("Gender")
                        .HasColumnType("int");

                    b.Property<DateTime>("InsertDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastName")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar");

                    b.Property<string>("Mobile")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("varchar");

                    b.Property<string>("NationalCode")
                        .HasMaxLength(10)
                        .HasColumnType("varchar");

                    b.Property<string>("Password")
                        .HasMaxLength(200)
                        .HasColumnType("varchar");

                    b.Property<string>("ProfilePic")
                        .HasColumnType("nvarchar(100)");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.Property<string>("SmsConfirmCode")
                        .IsRequired()
                        .HasMaxLength(5)
                        .HasColumnType("varchar");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("UniqeUserName")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(100)
                        .HasColumnType("varchar");

                    b.Property<string>("UserName")
                        .HasMaxLength(200)
                        .HasColumnType("varchar");

                    b.HasKey("Id");

                    b.ToTable("User", (string)null);
                });

            modelBuilder.Entity("Mogym.Domain.Entities.UserRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("InsertDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("UserRole", (string)null);
                });

            modelBuilder.Entity("Mogym.Domain.Entities.Workout", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("InsertDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("PlanId")
                        .HasColumnType("int");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("PlanId");

                    b.ToTable("Workout", (string)null);
                });

            modelBuilder.Entity("Mogym.Domain.Entities.Exercise", b =>
                {
                    b.HasOne("Mogym.Domain.Entities.ExerciseVideo", "ExerciseVideo")
                        .WithOne("Exercise")
                        .HasForeignKey("Mogym.Domain.Entities.Exercise", "ExerciseVideoId");

                    b.HasOne("Mogym.Domain.Entities.Exercise", "Exercise_Exercise")
                        .WithMany("Exercises")
                        .HasForeignKey("SuperSetId");

                    b.HasOne("Mogym.Domain.Entities.Workout", "Exercise_Workout")
                        .WithMany("Exercises")
                        .HasForeignKey("WorkoutId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ExerciseVideo");

                    b.Navigation("Exercise_Exercise");

                    b.Navigation("Exercise_Workout");
                });

            modelBuilder.Entity("Mogym.Domain.Entities.ExerciseSet", b =>
                {
                    b.HasOne("Mogym.Domain.Entities.Exercise", "Exercise_ExerciseSet")
                        .WithMany("ExerciseSets")
                        .HasForeignKey("ExerciseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Exercise_ExerciseSet");
                });

            modelBuilder.Entity("Mogym.Domain.Entities.Menu", b =>
                {
                    b.HasOne("Mogym.Domain.Entities.Menu", "Menu_Menu")
                        .WithMany("Menus")
                        .HasForeignKey("ParentId");

                    b.Navigation("Menu_Menu");
                });

            modelBuilder.Entity("Mogym.Domain.Entities.Permission", b =>
                {
                    b.HasOne("Mogym.Domain.Entities.Permission", "Permission_Permission")
                        .WithMany("Permissions")
                        .HasForeignKey("ParentId");

                    b.Navigation("Permission_Permission");
                });

            modelBuilder.Entity("Mogym.Domain.Entities.Plan", b =>
                {
                    b.HasOne("Mogym.Domain.Entities.Question", "AnsweQuestion_Plan")
                        .WithMany("Plans")
                        .HasForeignKey("AnserQuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Mogym.Domain.Entities.TrainerProfile", "TrainerProfile_Plan")
                        .WithMany("Plans")
                        .HasForeignKey("TrainerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Mogym.Domain.Entities.User", "User_Plan")
                        .WithMany("Plans")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AnsweQuestion_Plan");

                    b.Navigation("TrainerProfile_Plan");

                    b.Navigation("User_Plan");
                });

            modelBuilder.Entity("Mogym.Domain.Entities.RolePermission", b =>
                {
                    b.HasOne("Mogym.Domain.Entities.Permission", "RolePermission_Permission")
                        .WithMany("RolePermissions")
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Mogym.Domain.Entities.Role", "RolePermission_Role")
                        .WithMany("RolePermissions")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RolePermission_Permission");

                    b.Navigation("RolePermission_Role");
                });

            modelBuilder.Entity("Mogym.Domain.Entities.TrainerAchievement", b =>
                {
                    b.HasOne("Mogym.Domain.Entities.TrainerProfile", "TrainerAchievement_TrainerProfile")
                        .WithMany("TrainerAchievements")
                        .HasForeignKey("TrainerProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TrainerAchievement_TrainerProfile");
                });

            modelBuilder.Entity("Mogym.Domain.Entities.TrainerPlanCost", b =>
                {
                    b.HasOne("Mogym.Domain.Entities.TrainerProfile", "TrainerPlanCost_TrainerProfile")
                        .WithMany("TrainerPlanCosts")
                        .HasForeignKey("TrainerProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TrainerPlanCost_TrainerProfile");
                });

            modelBuilder.Entity("Mogym.Domain.Entities.TrainerProfile", b =>
                {
                    b.HasOne("Mogym.Domain.Entities.User", "User")
                        .WithOne("TrainerProfile")
                        .HasForeignKey("Mogym.Domain.Entities.TrainerProfile", "UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Mogym.Domain.Entities.UserRole", b =>
                {
                    b.HasOne("Mogym.Domain.Entities.Role", "UserRole_Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Mogym.Domain.Entities.User", "UserRole_User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserRole_Role");

                    b.Navigation("UserRole_User");
                });

            modelBuilder.Entity("Mogym.Domain.Entities.Workout", b =>
                {
                    b.HasOne("Mogym.Domain.Entities.Plan", "Plan_Workout")
                        .WithMany("Workouts")
                        .HasForeignKey("PlanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Plan_Workout");
                });

            modelBuilder.Entity("Mogym.Domain.Entities.Exercise", b =>
                {
                    b.Navigation("ExerciseSets");

                    b.Navigation("Exercises");
                });

            modelBuilder.Entity("Mogym.Domain.Entities.ExerciseVideo", b =>
                {
                    b.Navigation("Exercise")
                        .IsRequired();
                });

            modelBuilder.Entity("Mogym.Domain.Entities.Menu", b =>
                {
                    b.Navigation("Menus");
                });

            modelBuilder.Entity("Mogym.Domain.Entities.Permission", b =>
                {
                    b.Navigation("Permissions");

                    b.Navigation("RolePermissions");
                });

            modelBuilder.Entity("Mogym.Domain.Entities.Plan", b =>
                {
                    b.Navigation("Workouts");
                });

            modelBuilder.Entity("Mogym.Domain.Entities.Question", b =>
                {
                    b.Navigation("Plans");
                });

            modelBuilder.Entity("Mogym.Domain.Entities.Role", b =>
                {
                    b.Navigation("RolePermissions");

                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("Mogym.Domain.Entities.TrainerProfile", b =>
                {
                    b.Navigation("Plans");

                    b.Navigation("TrainerAchievements");

                    b.Navigation("TrainerPlanCosts");
                });

            modelBuilder.Entity("Mogym.Domain.Entities.User", b =>
                {
                    b.Navigation("Plans");

                    b.Navigation("TrainerProfile")
                        .IsRequired();

                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("Mogym.Domain.Entities.Workout", b =>
                {
                    b.Navigation("Exercises");
                });
#pragma warning restore 612, 618
        }
    }
}
