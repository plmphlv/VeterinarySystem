using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VeterinarySystem.Common;
using VeterinarySystem.Data.Domain.Entities;

namespace VeterinarySystem.Data.Domain.EntityConfigurations
{
    public class AnimalConfiguration : IEntityTypeConfiguration<Animal>
    {
        public void Configure(EntityTypeBuilder<Animal> builder)
        {
            builder.HasKey(a => a.AnimalId);

            builder.Property(a => a.Name)
                .HasMaxLength(EntityConstants.AmnimalNameMaxLength)
                .IsRequired(false)
                .IsUnicode(true);

            builder.Property(a => a.Weight)
                .IsRequired(true);

            builder.Property(a => a.AnimalType)
                .IsRequired(true);

            builder.HasMany(a => a.Procedures)
                .WithOne(p => p.Animal);

            builder.HasMany(a => a.Procedures)
                .WithOne(p => p.Animal);

            builder.HasOne(a => a.AnimalOwner)
                .WithMany(ao => ao.Animals);

        }
    }
}
