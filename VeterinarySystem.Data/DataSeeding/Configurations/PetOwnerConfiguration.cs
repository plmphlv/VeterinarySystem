using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VeterinarySystem.Data.Domain.DataSeed;
using VeterinarySystem.Data.Domain.Entities;

namespace VeterinarySystem.Data.DataSeeding.Configurations
{
	internal class PetOwnerConfiguration : IEntityTypeConfiguration<AnimalOwner>
	{
		public void Configure(EntityTypeBuilder<AnimalOwner> builder)
		{
			builder.HasData(DataSeed.SeedOwner());
		}
	}
}
