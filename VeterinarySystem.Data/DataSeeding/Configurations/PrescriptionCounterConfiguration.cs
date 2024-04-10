using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using VeterinarySystem.Data.Domain.DataSeed;
using VeterinarySystem.Data.Domain.Entities;

namespace VeterinarySystem.Data.DataSeeding.Configurations
{
	internal class PrescriptionCounterConfiguration : IEntityTypeConfiguration<PrescriptionCounter>
	{
		public void Configure(EntityTypeBuilder<PrescriptionCounter> builder)
		{
			builder.HasData(DataSeed.SeedCounter());
		}
	}
}
