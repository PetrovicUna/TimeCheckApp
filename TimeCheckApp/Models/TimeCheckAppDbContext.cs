using Microsoft.EntityFrameworkCore;


namespace TimeCheckApp.Models
{
    public class TimeCheckAppDbContext : DbContext
    {
        public TimeCheckAppDbContext(DbContextOptions<TimeCheckAppDbContext> options)
           : base(options)
        {
        }

        public DbSet<Person> Persons { get; set; }
        public DbSet<WorkingHours> WorkingHourses { get; set; }
        public DbSet<Tasks> Tasks { get; set; }
        public DbSet<Projects> Projects { get; set; }
        public DbSet<Absences> Absences { get; set; }
        public DbSet<Grades> Grades { get; set; }
        public DbSet<ProjectTask> ProjectTasks { get; set; }
        public DbSet<TemporaryData> TemporaryData { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>()
                .HasMany(c => c.Absences)
                .WithOne(e => e.Person)
                .HasForeignKey(e => e.PersonID);

            modelBuilder.Entity<Tasks>()
               .HasMany(t => t.WorkingHours)
               .WithOne(e => e.Tasks)
               .HasForeignKey(e => e.TaskID);

            modelBuilder.Entity<ProjectTask>()
                .HasOne(pt => pt.Project)
                .WithMany(p => p.ProjectTasks)
                .HasForeignKey(pt => pt.ProjectID);

            modelBuilder.Entity<ProjectTask>()
                .HasOne(pt => pt.Tasks)
                .WithMany(t => t.ProjectTasks)
                .HasForeignKey(pt => pt.TaskID);

            modelBuilder.Entity<Person>()
                .HasIndex(p => p.PersonNumber)
                .IsUnique();
        }
    }
}