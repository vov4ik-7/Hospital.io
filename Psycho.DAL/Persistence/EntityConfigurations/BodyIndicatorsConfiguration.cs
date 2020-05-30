using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Psycho.DAL.Core.Domain;

namespace Psycho.DAL.Persistence.EntityConfigurations
{
    public class BodyIndicatorsConfiguration : IEntityTypeConfiguration<BodyIndicator>
    {
        public void Configure(EntityTypeBuilder<BodyIndicator> builder)
        {
            builder.HasKey(bodyIndicator => bodyIndicator.Id);
        }
    }
}
