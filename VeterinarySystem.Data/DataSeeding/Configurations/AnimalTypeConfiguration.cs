using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using VeterinarySystem.Data.Domain.DataSeed;
using VeterinarySystem.Data.Domain.Entities;

namespace VeterinarySystem.Data.DataSeeding.Configurations
{
	public class AnimalTypeConfiguration : IEntityTypeConfiguration<AnimalType>
	{
		public void Configure(EntityTypeBuilder<AnimalType> builder)
		{
			builder.HasData(DataSeed.SeedAnimalTypes());
		}
	}
}
