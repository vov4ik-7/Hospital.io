using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Psycho.DAL.Core.Domain;

namespace Psycho.DAL.Persistence.EntityConfigurations
{
    public class WorkScheduleConfiguration : IEntityTypeConfiguration<WorkSchedule>
    {
        public void Configure(EntityTypeBuilder<WorkSchedule> builder)
        {
            builder.HasKey(ws => ws.Id);

            builder.HasIndex(i => new { i.PsychologistId, i.Day })
                    .IsUnique();

            builder.HasOne(ws => ws.Psychologist)
                .WithMany(p => p.WorkSchedules)
                .HasForeignKey(ws => ws.PsychologistId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            char[] separators = { ':' };

            var timeConverter = new ValueConverter<Time, string>(
                v => v.ToString(),
                v => new Time {
                    Hours = Int32.Parse(v.Split(separators, System.StringSplitOptions.RemoveEmptyEntries)[0]),
                    Minutes = Int32.Parse(v.Split(separators, System.StringSplitOptions.RemoveEmptyEntries)[1]),
                    Seconds = Int32.Parse(v.Split(separators, System.StringSplitOptions.RemoveEmptyEntries)[2])
                });

            builder.Property(ws => ws.StartTime)
                .HasConversion(timeConverter);

            builder.Property(ws => ws.EndTime)
                .HasConversion(timeConverter);

            var dayConverter = new ValueConverter<Day, string>(
                v => v.ToString(),
                v => (Day)Enum.Parse(typeof(Day), v));

            builder.Property(ws => ws.Day)
                .HasConversion(dayConverter);
        }
    }
}
