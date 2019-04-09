using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Psycho.DAL.Core.Domain;

namespace Psycho.DAL.Persistence.EntityConfigurations
{
    public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.HasKey(appointment => appointment.Id);

            builder.HasOne(appointment => appointment.Psychologist)
                .WithMany(user => user.Appointments)
                .HasForeignKey(appointment => appointment.PsychologistId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            builder.HasOne(appointment => appointment.AuthorizedUser)
                .WithMany(user => user.Appointments)
                .HasForeignKey(appointment => appointment.AuthorizedUserId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            builder.HasOne(appointment => appointment.Result)
                .WithOne(result => result.Appointment)
                .HasForeignKey<AppointmentResult>(ar => ar.AppointmentId);
        }
    }
}
