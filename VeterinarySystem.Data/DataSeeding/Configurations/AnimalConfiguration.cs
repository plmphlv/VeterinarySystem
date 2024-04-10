using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VeterinarySystem.Data.Domain.DataSeed;
using VeterinarySystem.Data.Domain.Entities;

namespace VeterinarySystem.Data.DataSeeding.Configurations
{
	public class AnimalConfiguration : IEntityTypeConfiguration<Animal>
	{
		public void Configure(EntityTypeBuilder<Animal> builder)
		{
			builder.HasData(DataSeed.SeedAnimals());
		}
	}
}
