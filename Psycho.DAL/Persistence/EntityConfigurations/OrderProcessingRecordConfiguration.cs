using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Psycho.DAL.Core.Domain;

namespace Psycho.DAL.Persistence.EntityConfigurations
{
    public class OrderProcessingRecordConfiguration: IEntityTypeConfiguration<OrderProcessingRecord>
    {
        public void Configure(EntityTypeBuilder<OrderProcessingRecord> builder)
        {
            builder.HasKey(record => record.Id);
        }
    }
}
