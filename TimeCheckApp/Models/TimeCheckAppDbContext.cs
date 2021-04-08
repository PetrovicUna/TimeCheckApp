using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeCheckApp.Models
{
    public class TimeCheckAppDbContext : DbContext
    {
        public TimeCheckAppDbContext(DbContextOptions<TimeCheckAppDbContext> options)
           : base(options)
        {
        }

        //public DbSet<Person> Persons { get; set; }
        //public DbSet<WorkingHours> WorkingHourses { get; set; }
        //public DbSet<Tasks> Tasks { get; set; }
        //public DbSet<Projects> Projects { get; set; }
        //public DbSet<Absences> Absences { get; set; }
        //public DbSet<Grades> Grades { get; set; }
        //public DbSet<ProjectTask> ProjectTasks { get; set; }
        public DbSet<TemporaryData> TemporaryData { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Person>()
            //    .HasMany(c => c.WorkingHours)
            //    .WithOne(e => e.Person)
            //    .HasForeignKey(e => e.PersonID);

            //modelBuilder.Entity<Tasks>()
            //   .HasMany(t => t.WorkingHours)
            //   .WithOne(e => e.Tasks)
            //   .HasForeignKey(e => e.TaskID);

            //modelBuilder.Entity<Grades>()
            //  .HasOne(t => t.Person)
            //  .WithOne(e => e.Grades)
            //  .HasForeignKey<Person>(e => e.GradeCode);

            //modelBuilder.Entity<ProjectTask>()
            //    .HasKey(pt => new { pt.ProjectID, pt.TaskID });

            //modelBuilder.Entity<ProjectTask>()
            //    .HasOne(pt => pt.Project)
            //    .WithMany(p => p.ProjectTasks)
            //    .HasForeignKey(pt => pt.ProjectID);

            //modelBuilder.Entity<ProjectTask>()
            //    .HasOne(pt => pt.Tasks)
            //    .WithMany(t => t.ProjectTasks)
            //    .HasForeignKey(pt => pt.TaskID);

            //modelBuilder.Entity<PersonAbsences>()
            //   .HasKey(pa => new { pa.AbsenceID, pa.PersonID });

            //modelBuilder.Entity<PersonAbsences>()
            //    .HasOne(pa => pa.Person)
            //    .WithMany(p => p.PersonAbsences)
            //    .HasForeignKey(pa => pa.PersonID);

            //modelBuilder.Entity<PersonAbsences>()
            //    .HasOne(pa => pa.Absences)
            //    .WithMany(a => a.PersonAbsences)
            //    .HasForeignKey(pa => pa.AbsenceID);
        }
    }
}

//hyperium express service file