using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using VeterinarySystem.Data.Domain.Entities;

namespace VeterinarySystem.Data
{
	public class VeterinarySystemDbContext : IdentityDbContext<StaffMember>
    {
        public VeterinarySystemDbContext(DbContextOptions<VeterinarySystemDbContext> options) : base(options)
        {

        }

        public DbSet<AnimalOwner> AnimalOwners { get; set; } = null!;
        public DbSet<Animal> Animals { get; set; } = null!;
        public DbSet<Appointment> Appointments { get; set; } = null!;
        public DbSet<Procedure> Procedures { get; set; } = null!;
        public DbSet<Prescription> Prescriptions { get; set; } = null!;
        public DbSet<AnimalType> AnimalTypes { get; set; } = null!;
        public DbSet<PrescriptionCounter> PrescriptionCounters { get; set; } = null!;



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
