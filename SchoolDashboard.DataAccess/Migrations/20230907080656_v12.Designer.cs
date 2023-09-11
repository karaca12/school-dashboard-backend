﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SchoolDashboard.DataAccess;

namespace SchoolDashboard.DataAccess.Migrations
{
    [DbContext(typeof(SchoolDbContext))]
    [Migration("20230907080656_v12")]
    partial class v12
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SchoolDasboard.Model.Lecture", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("LectureDescription")
                        .IsRequired();

                    b.Property<string>("LectureName")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Lectures");
                });

            modelBuilder.Entity("SchoolDasboard.Model.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("UserBirthdate");

                    b.Property<string>("UserEmail")
                        .IsRequired();

                    b.Property<string>("UserName")
                        .IsRequired();

                    b.Property<string>("UserNationalIdentity")
                        .IsRequired()
                        .HasMaxLength(11);

                    b.Property<string>("UserPassword")
                        .IsRequired();

                    b.Property<string>("UserPhone")
                        .IsRequired();

                    b.Property<string>("UserRole")
                        .IsRequired();

                    b.Property<string>("UserSchoolNumber")
                        .IsRequired()
                        .HasMaxLength(9);

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("SchoolDasboard.Model.UserLecture", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("LectureId");

                    b.HasKey("UserId", "LectureId");

                    b.HasIndex("LectureId");

                    b.ToTable("UserLectures");
                });

            modelBuilder.Entity("SchoolDasboard.Model.UserLecture", b =>
                {
                    b.HasOne("SchoolDasboard.Model.Lecture", "Lecture")
                        .WithMany("Users")
                        .HasForeignKey("LectureId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SchoolDasboard.Model.User", "User")
                        .WithMany("Lectures")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
