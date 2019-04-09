using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Psycho.DAL.Core.Domain;

namespace Psycho.DAL.Persistence.EntityConfigurations
{
    public class AppointmentResultConfiguration : IEntityTypeConfiguration<AppointmentResult>
    {
        public void Configure(EntityTypeBuilder<AppointmentResult> builder)
        {
            builder.HasKey(appointmentResult => appointmentResult.Id);
        }
    }
}
