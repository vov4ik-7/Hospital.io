using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Psycho.DAL.Core.Domain;

namespace Psycho.DAL.Persistence.EntityConfigurations
{
    public class HealthDiaryConfiguration : IEntityTypeConfiguration<HealthDiary>
    {
        public void Configure(EntityTypeBuilder<HealthDiary> builder)
        {
            builder.HasKey(healthDiary => healthDiary.Id);
        }
    }
}
