using System;
using Microsoft.EntityFrameworkCore;
using SchoolDasboard.Model;

namespace SchoolDashboard.DataAccess
{
    public class SchoolDbContext:DbContext
    {
        public SchoolDbContext()
        {
        }
        public SchoolDbContext(DbContextOptions<SchoolDbContext> options):base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Lecture> Lectures { get; set; }
        public DbSet<UserLecture> UserLectures { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=GVY906\\SQLEXPRESS;Database=School_Web_Db_12;Trusted_Connection=True");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserLecture>()
                .HasKey(ul => new { ul.UserId, ul.LectureId });
            modelBuilder.Entity<UserLecture>()
                .HasOne(ul => ul.User)
                .WithMany(u => u.Lectures)
                .HasForeignKey(ul=>ul.UserId);
            modelBuilder.Entity<UserLecture>()
                .HasOne(ul => ul.Lecture)
                .WithMany(u => u.Users)
                .HasForeignKey(ul => ul.LectureId);
        }
    }
}
