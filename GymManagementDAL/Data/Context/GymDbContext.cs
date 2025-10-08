﻿using GymManagementDAL.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Data.Context
{
    public class GymDbContext : DbContext

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
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(GymDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
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
