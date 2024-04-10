using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VeterinarySystem.Data.Domain.DataSeed;
using VeterinarySystem.Data.Domain.Entities;

namespace VeterinarySystem.Data.DataSeeding.Configurations
{
	public class ProcedureConfiguration : IEntityTypeConfiguration<Procedure>
	{
		public void Configure(EntityTypeBuilder<Procedure> builder)
		{
			builder.HasData(DataSeed.SeedProcedure());
		}
	}
}
