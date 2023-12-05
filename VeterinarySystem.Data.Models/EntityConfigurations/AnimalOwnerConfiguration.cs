using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VeterinarySystem.Common;
using VeterinarySystem.Data.Domain.Entities;

namespace VeterinarySystem.Data.Domain.EntityConfigurations
{
    public class AnimalOwnerConfiguration : IEntityTypeConfiguration<AnimalOwner>

    {
        public void Configure(EntityTypeBuilder<AnimalOwner> builder)
        {
            builder.HasKey(ao => ao.AnimalOwnerId);

            builder.Property(ao => ao.OwnerFirstName)
                .HasMaxLength(EntityConstants.HumanNameMaxLength)
                .IsRequired(true)
                .IsUnicode(true);

            builder.Property(ao => ao.OwnerLastName)
                .HasMaxLength(EntityConstants.HumanNameMaxLength)
                .IsRequired(true)
                .IsUnicode(true);

            builder.Property(ao => ao.PhoneNumber)
                .HasMaxLength(EntityConstants.PhoneNumberMaxLength)
                .IsRequired(true);

            builder.HasMany(ao => ao.Animals)
                .WithOne(a => a.AnimalOwner);

            builder.HasMany(ao => ao.Appointments)
                .WithOne(a => a.AnimalOwner);

        }
    }
}
