using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VeterinarySystem.Common;
using VeterinarySystem.Data.Domain.Entities;

namespace VeterinarySystem.Data.Domain.EntityConfigurations
{
    public class StaffMemberConfiguration : IEntityTypeConfiguration<StaffMember>
    {
        public void Configure(EntityTypeBuilder<StaffMember> builder)
        {
            builder.HasKey(sm => sm.StaffMemberId);

            builder.Property(sm => sm.StaffMemberFirstName)
                .HasMaxLength(EntityConstants.HumanNameMaxLength)
                .IsRequired(true)
                .IsUnicode(true);

            builder.Property(sm => sm.StaffMemberLasttName)
                .HasMaxLength(EntityConstants.HumanNameMaxLength)
                .IsRequired(true)
                .IsUnicode(true);

            builder.Property(sm => sm.PhoneNumter)
                .HasMaxLength(EntityConstants.PhoneNumberMaxLength)
                .IsRequired(true)
                .IsUnicode(true);

            builder.Property(sm => sm.StaffType)
                .IsRequired(true);

            builder.Property(sm => sm.StaffPositionName)
                .HasMaxLength(EntityConstants.StaffPositionNameMaxLength)
                .IsRequired(true)
                .IsUnicode(true);
        }
    }
}
