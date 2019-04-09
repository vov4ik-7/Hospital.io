using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Psycho.DAL.Core.Domain;

namespace Psycho.DAL.Persistence.EntityConfigurations
{
    public class ReportConfiguration : IEntityTypeConfiguration<Report>
    {
        public void Configure(EntityTypeBuilder<Report> builder)
        {
            builder.HasKey(report => report.Id);

            builder.HasOne(report => report.AuthorizedUser)
                .WithMany(au => au.Reports)
                .HasForeignKey(report => report.AuthorizedUserId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            builder.HasOne(report => report.Psychologist)
                .WithMany(p => p.Reports)
                .HasForeignKey(report => report.PsychologistId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();
        }
    }
}
