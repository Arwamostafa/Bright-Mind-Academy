using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Repository
{
    public class AppDbContext: IdentityDbContext <ApplicationUser, ApplicationRole, int>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentClassSubject>()
                .HasKey(s => new { s.SubjectID, s.TrackID, s.ClassID, s.InstructorID });

            modelBuilder.Entity<StudentClassSubject>()
                .HasOne(s => s.Instructor)
                .WithMany(i=> i.StudentClassSubjects)
                .HasForeignKey(s => s.InstructorID)
                .OnDelete(DeleteBehavior.Restrict); // Or DeleteBehavior.NoAction if i want to handle manually

            modelBuilder.Entity<Subject>()
               .HasOne(s => s.Instructor)
               .WithMany(i => i.Subjects)
               .HasForeignKey(s => s.InstructorID)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<StudentClassSubject>()
                .HasOne(s => s.Class)
                .WithMany(c=>c.StudentClassSubjects)
                .HasForeignKey(s => s.ClassID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<StudentClassSubject>()
                .HasOne(s => s.Track)
                .WithMany(t=>t.StudentClassSubjects)
                .HasForeignKey(s => s.TrackID)
                .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<StudentClassSubject>()
            //    .HasOne(s => s.SubjectProfile)
            //    .WithMany(s=>s.StudentClassSubjects)
            //    .HasForeignKey(s => s.SubjectID)
            //    .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Lesson>().Property(l => l.Title).IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Lesson>().HasKey(s => s.Id);

            modelBuilder.Entity<Lesson>().Property(l => l.Description)
                .HasMaxLength(700);

            modelBuilder.Entity<Lesson>().Property(l => l.VideoUrl)
                .HasMaxLength(700);
            modelBuilder.Entity<Lesson>().Property(l => l.PdfUrl)
                .HasMaxLength(700);

            modelBuilder.Entity<Lesson>().Property(l => l.AssigmentUrl)
                .HasMaxLength(700);

            modelBuilder.Entity<Lesson>().HasOne(l => l.Unit)
                .WithMany(u => u.Lessons)
                .HasForeignKey(l => l.UnitId)
                .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Cascade);
            modelBuilder.Entity<Subject>()
                .Property(s => s.Price)
                .HasColumnType("decimal(10,2)");
            modelBuilder.Entity<Subject>().HasMany(s => s.Units)
                .WithOne(u => u.Subject)
                .HasForeignKey(u => u.SubjectId)
                .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Cascade);
            modelBuilder.Entity<Unit>().Property(u => u.Title).IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Unit>().Property(u => u.Description)
                .HasMaxLength(700);


            modelBuilder.Entity<Unit>().HasOne(u => u.Subject).WithMany(s => s.Units).HasForeignKey(u => u.SubjectId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Unit>().HasMany(u => u.Lessons)
                .WithOne(l => l.Unit)
                .HasForeignKey(l => l.UnitId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Unit>()
                .Property(e => e.Id)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Lesson>()
                .Property(e => e.Id)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<SubjectStudent>()
                .HasKey(ss => new { ss.SubjectId, ss.StudentId });

            modelBuilder.Entity<SubjectStudent>()
                .HasOne(ss => ss.Student)
                .WithMany(s => s.subjectStudents)
                .HasForeignKey(ss => ss.StudentId);

            modelBuilder.Entity<SubjectStudent>()
                .HasOne(ss => ss.Subject)
                .WithMany(s => s.subjectStudents)
                .HasForeignKey(ss => ss.SubjectId);

            modelBuilder.Entity<Lesson>()
                .HasOne(l => l.Quiz)
                .WithOne(q => q.Lesson)
                .HasForeignKey<quiz>(q => q.LessonId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Subject)
                .WithMany(s => s.Payments)
                .HasForeignKey(p => p.SubjectID);

            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Student)
                .WithMany(u => u.Payments)
                .HasForeignKey(p => p.StudentID);

            modelBuilder.Entity<Payment>()
                .HasIndex(p => new { p.StudentID, p.SubjectID })
                .IsUnique();
            base.OnModelCreating(modelBuilder);
        }


        public virtual DbSet<StudentProfile> StudentProfiles { get; set; }
        public virtual DbSet<InstructorProfile> InstructorProfiles { get; set; }
        public virtual DbSet<AdminProfile> AdminProfiles { get; set; }

        public virtual DbSet<Class> Classes { get; set; }
        //public virtual DbSet<Instructor> Instructors { get; set; }
        public virtual DbSet<Track> Tracks { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }
        //public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<StudentClassSubject> StudentClassSubjects { get; set; }
        public DbSet<Payment> Payments { get; set; }

        public virtual DbSet<quiz> Quizzes { get; set; }
        public virtual DbSet<question> Questions { get; set; }
        public virtual DbSet<option> Options { get; set; }

        public virtual DbSet<Unit> Units { get; set; }

        public virtual DbSet<SubjectStudent> SubjectStudents { get; set; }

        public virtual DbSet<Chat> Chats  {get; set; }

        public virtual DbSet<Lesson> Lessons { get; set; }


    }
}
