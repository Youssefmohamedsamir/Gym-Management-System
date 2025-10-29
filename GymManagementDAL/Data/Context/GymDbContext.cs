using GymManagementDAL.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Data.Context
{
    public class GymDbContext : IdentityDbContext<ApplicationUser>

    {

        public GymDbContext(DbContextOptions<GymDbContext>options) : base(options)
        {

        }

        //override protected void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=.;Database=GymManagementDB;Trusted_Connection=True;TrustServerCertificate=True;");
        //    base.OnConfiguring(optionsBuilder);
        //}
        override protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(builder: modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(GymDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ApplicationUser>(Eb =>
            {
                Eb.Property(X => X.FirstName)
                .HasColumnType("varchar")
                .HasMaxLength(50);

                Eb.Property(X => X.LastName)
               .HasColumnType("varchar")
               .HasMaxLength(50);
            });
        }

        #region Dbsets

        
        public DbSet<Member> Members { get; set; }
        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<MemberSession> MemberSessions { get; set; }
        public DbSet<MemberShip> MemberShips { get; set; }
        public DbSet<HealthRecord> HealthRecords { get; set; }
        public DbSet<Plan> Plans { get; set; }




        #endregion
    }
}
