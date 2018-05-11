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
            // Direction
            modelBuilder.Entity<Direction>().HasKey(k => k.Code);
            modelBuilder.Entity<Direction>().HasMany(p => p.Profiles).WithOne(d => d.Direction).HasForeignKey(k => k.DirectionCode).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Direction>().HasMany(c => c.Competencies).WithOne(d => d.Direction).HasForeignKey(k => k.DirectionCode);

            // Profile
            modelBuilder.Entity<Profile>().HasKey(k => k.Code);
            modelBuilder.Entity<Profile>().HasOne(p => p.Direction).WithMany(d => d.Profiles).HasForeignKey(k => k.DirectionCode).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Profile>().HasMany(p => p.Plans).WithOne(d => d.Profile).HasForeignKey(k => k.ProfileCode).OnDelete(DeleteBehavior.Cascade);

            // Plan
            modelBuilder.Entity<Plan>().HasKey(k => k.Code);
            modelBuilder.Entity<Plan>().HasMany(p => p.Disciplines).WithOne(d => d.Plan).HasForeignKey(k => k.PlanCode).OnDelete(DeleteBehavior.Cascade);

            // Discipline
            modelBuilder.Entity<Discipline>().HasKey(k => k.Code);
            modelBuilder.Entity<Discipline>().HasMany(di => di.DisciplinesInfo).WithOne(d => d.Discipline).HasForeignKey(k => k.DisciplineCode);

            // DisciplineInfo
            modelBuilder.Entity<DisciplineInfo>().HasKey(k => k.Id);
            modelBuilder.Entity<DisciplineInfo>().HasOne(p => p.Discipline).WithMany(d => d.DisciplinesInfo).HasForeignKey(k => k.DisciplineCode);
            modelBuilder.Entity<DisciplineInfo>().HasOne(p => p.Plan).WithMany(d => d.Disciplines).HasForeignKey(k => k.PlanCode);

            // Competence
            modelBuilder.Entity<Competence>().HasKey(k => k.Id);

            // ProfileCompetence
            modelBuilder.Entity<ProfileCompetence>().HasKey(k => new { k.ProfileCode, k.CompetenceId });
            modelBuilder.Entity<ProfileCompetence>().HasOne(p => p.Profile).WithMany(d => d.Competencies).HasForeignKey(k => k.ProfileCode);
            modelBuilder.Entity<ProfileCompetence>().HasOne(p => p.Competence).WithMany(d => d.Profiles).HasForeignKey(k => k.CompetenceId);

            // DisciplineCompetence
            modelBuilder.Entity<DisciplineCompetence>().HasKey(k => new { k.DisciplineInfoId, k.CompetenceId });
            modelBuilder.Entity<DisciplineCompetence>().HasOne(p => p.DisciplineInfo).WithMany(d => d.Competencies).HasForeignKey(k => k.DisciplineInfoId);
            modelBuilder.Entity<DisciplineCompetence>().HasOne(p => p.Competence).WithMany(d => d.Disciplines).HasForeignKey(k => k.CompetenceId);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Direction> Direction { get; set; }

        public DbSet<Competence> Competence { get; set; }

        public DbSet<Profile> Profile { get; set; }

        public DbSet<Plan> Plan { get; set; }

        public DbSet<Discipline> Discipline { get; set; }

        public DbSet<DisciplineInfo> DisciplineInfo { get; set; }

        public DbSet<RPDControlSystem.Models.RPD.ProfileCompetence> ProfileCompetence { get; set; }

        public DbSet<RPDControlSystem.Models.RPD.DisciplineCompetence> DisciplineCompetence { get; set; }
    }
}
