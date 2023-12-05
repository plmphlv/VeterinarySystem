﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VeterinarySystem.Common;
using VeterinarySystem.Data.Domain.Entities;

namespace VeterinarySystem.Data.Domain.EntityConfigurations
{
    public class MedicineConfiguration : IEntityTypeConfiguration<Medicine>
    {
        public void Configure(EntityTypeBuilder<Medicine> builder)
        {
            builder.HasKey(med => med.MedicineId);

            builder.Property(med => med.Name)
                .HasMaxLength(EntityConstants.MedicineNameMaxLength)
                .IsRequired(true)
                .IsUnicode(true);

            builder.Property(per => per.ProductionDate)
                .IsRequired(true);

            builder.Property(per => per.ExpiryDate)
                .IsRequired(true);

            builder.Property(med => med.Producer)
                .HasMaxLength(EntityConstants.MedicineProducerNameMaxLength)
                .IsRequired(true)
                .IsUnicode(true);

            builder.HasOne(per => per.Prescription)
                .WithMany(med => med.Medicines);


        }
    }
}
