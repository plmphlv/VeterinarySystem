using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VeterinarySystem.Common;
using VeterinarySystem.Data.Domain.Entities;

namespace VeterinarySystem.Data.Domain.EntityConfigurations
{
    public class ProcedureConfiguration : IEntityTypeConfiguration<Procedure>
    {
        public void Configure(EntityTypeBuilder<Procedure> builder)
        {
            builder.HasKey(p => p.ProcedureId);

            builder.Property(p => p.Name)
                .IsRequired(true)
                .HasMaxLength(EntityConstants.ProcedureNameMaxLength);

            builder.Property(p => p.ProcedureDescription)
                .HasMaxLength(EntityConstants.DescriptionMaxLength)
                .IsRequired(false)
                .IsUnicode(true);

            builder.Property(p => p.IsMedical)
                .IsRequired(true);

            builder.HasOne(p => p.StaffMember)
                .WithMany(sm => sm.Procedures);

            builder.HasOne(p => p.Animal)
                .WithMany(a => a.Procedures);
        }
    }
}
