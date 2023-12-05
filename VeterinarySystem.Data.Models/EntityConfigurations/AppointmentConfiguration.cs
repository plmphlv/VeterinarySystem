using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VeterinarySystem.Data.Domain.Entities;

namespace VeterinarySystem.Data.Domain.EntityConfigurations
{
    public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.HasKey(ap => ap.AppointmentId);

            builder.Property(ap => ap.AppointmentDate)
                .IsRequired(true);

            builder.HasOne(ap => ap.AnimalOwner)
                .WithMany(ap => ap.Appointments);

            builder.HasOne(ap => ap.StaffMember)
                .WithMany(sm => sm.Appointments);
        }
    }
}
