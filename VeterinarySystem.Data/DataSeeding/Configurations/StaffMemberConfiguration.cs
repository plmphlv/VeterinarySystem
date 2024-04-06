using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VeterinarySystem.Data.Domain.Entities;

namespace VeterinarySystem.Data.Domain.DataSeed.Configurations
{
    public class StaffMemberConfiguration : IEntityTypeConfiguration<StaffMember>
    {
        public void Configure(EntityTypeBuilder<StaffMember> builder)
        {
            DataSeed dataSeed = new DataSeed();

            builder.HasData([dataSeed.Administrator, dataSeed.Vet]);

        }
    }
}
