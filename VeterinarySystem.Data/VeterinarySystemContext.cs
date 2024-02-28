using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;
using VeterinarySystem.Data.Domain.Entities;

namespace VeterinarySystem.Data
{
    public class VeterinarySystemContext : IdentityDbContext<StaffMember>
    {
        public VeterinarySystemContext(DbContextOptions<VeterinarySystemContext> options) : base(options)
        {

        }


        public DbSet<StaffMember> StaffMembers { get; set; } = null!;
        public DbSet<AnimalOwner> AnimalOwners { get; set; } = null!;
        public DbSet<Animal> Animals { get; set; } = null!;
        public DbSet<Appointment> Appointments { get; set; } = null!;
        public DbSet<Procedure> Procedures { get; set; } = null!;
        public DbSet<Prescription> Prescriptions { get; set; } = null!;
        public DbSet<Medicine> Medicines { get; set; } = null!;



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
    }

}
