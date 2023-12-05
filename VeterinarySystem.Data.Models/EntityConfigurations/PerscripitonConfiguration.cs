using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VeterinarySystem.Common;
using VeterinarySystem.Data.Domain.Entities;

namespace VeterinarySystem.Data.Domain.EntityConfigurations
{
    public class PerscripitonConfiguration : IEntityTypeConfiguration<Prescription>
    {
        public void Configure(EntityTypeBuilder<Prescription> builder)
        {
            builder.HasKey(per => per.PrescriptionId);

            builder.Property(per => per.Description)
                .HasMaxLength(EntityConstants.DescriptionMaxLength)
                .IsRequired(false)
                .IsUnicode(true);

            builder.Property(per => per.IssueDate)
                .IsRequired(true);

            builder.HasOne(per => per.StaffMember)
                .WithMany(sm => sm.Prescriptions);

            

            //Ask how to configure Medicines
        }
    }
}
