using Microsoft.EntityFrameworkCore;
using RPDControlSystem.Models.RPD;

namespace RPDControlSystem.Storage
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Composite keys

            modelBuilder.Entity<DisciplineInfo>().HasKey(k => new { k.PlanCode, k.DisciplineCode });

            // Relationship

            // Direction
            modelBuilder.Entity<Direction>().HasMany(p => p.Profiles).WithOne(d => d.Directions).HasForeignKey(k => k.DirectionCode).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Direction>().HasMany(c => c.Competencies).WithOne(d => d.Direction).HasForeignKey(k => k.DirectionCode);

            // Profile
            modelBuilder.Entity<Profile>().HasOne(p => p.Directions).WithMany(d => d.Profiles).HasForeignKey(k => k.DirectionCode).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Profile>().HasMany(p => p.Plans).WithOne(d => d.Profile).HasForeignKey(k => k.ProfileCode).OnDelete(DeleteBehavior.Cascade);
            //modelBuilder.Entity<Profile>().HasMany(c => c.Competencies).WithOne(d => d.Competence).HasForeignKey(k => k.ProfileCode);

            // Plan
            modelBuilder.Entity<Plan>().HasMany(p => p.Disciplines).WithOne(d => d.Plan).HasForeignKey(k => k.PlanCode).OnDelete(DeleteBehavior.Cascade);

            // Discipline
            modelBuilder.Entity<Discipline>().HasMany(di => di.DisciplinesInfo).WithOne(d => d.Discipline).HasForeignKey(k => k.DisciplineCode);

            // DisciplineInfo
            modelBuilder.Entity<DisciplineInfo>().HasOne(p => p.Discipline).WithMany(d => d.DisciplinesInfo).HasForeignKey(k => k.DisciplineCode);

            // DirectionCompetence
            modelBuilder.Entity<DirectionCompetence>().HasKey(k => new { k.DirectionCode, k.CompetenceId });
            modelBuilder.Entity<DirectionCompetence>().HasOne(p => p.Direction).WithMany(d => d.Competencies).HasForeignKey(k => k.DirectionCode);

            // ProfileCompetence
            modelBuilder.Entity<ProfileCompetence>().HasKey(k => new { k.ProfileCode, k.CompetenceId });
            modelBuilder.Entity<ProfileCompetence>().HasOne(p => p.Profile).WithMany(d => d.Competencies).HasForeignKey(k => k.ProfileCode);

            // DisciplineCompetence
            modelBuilder.Entity<DisciplineCompetence>().HasKey(k => new { k.DisciplineCode, k.CompetenceId });
            modelBuilder.Entity<DisciplineCompetence>().HasOne(p => p.Discipline).WithMany(d => d.Competencies).HasForeignKey(k => new { k.DisciplineCode, k.PlanCode});


            base.OnModelCreating(modelBuilder);
        }

        public DbSet<RPDControlSystem.Models.RPD.Direction> Direction { get; set; }

        public DbSet<RPDControlSystem.Models.RPD.Competence> Competence { get; set; }

        public DbSet<RPDControlSystem.Models.RPD.DirectionCompetence> DirectionCompetence { get; set; }
    }
}
