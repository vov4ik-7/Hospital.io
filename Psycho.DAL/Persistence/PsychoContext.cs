using Microsoft.EntityFrameworkCore;
using Psycho.DAL.Core.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Psycho.DAL.Persistence.EntityConfigurations;

namespace Psycho.DAL.Persistence
{
    public class PsychoContext : IdentityDbContext<User, Role, int>
    {
        public PsychoContext() { }
        public PsychoContext(DbContextOptions<PsychoContext> options) : base(options) { }

        public virtual DbSet<Psychologist> Psychologists { get; set; }
        public virtual DbSet<AuthorizedUser> AuthorizedUsers { get; set; }
        public virtual DbSet<AnonymousUser> AnonymousUsers { get; set; }
        public virtual DbSet<Appointment> Appointments { get; set; }
        public virtual DbSet<AppointmentResult> AppointmentResults { get; set; }
        public virtual DbSet<Report> Reports { get; set; }
        public virtual DbSet<WorkSchedule> WorkSchedules { get; set; }
        public virtual DbSet<Chat> Chats { get; set; }
        public virtual DbSet<Analysis> Analyses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new RoleConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new AppointmentConfiguration());
            builder.ApplyConfiguration(new AppointmentResultConfiguration());
            builder.ApplyConfiguration(new ReportConfiguration());
            builder.ApplyConfiguration(new ChatConfiguration());
            builder.ApplyConfiguration(new WorkScheduleConfiguration());
            builder.ApplyConfiguration(new AnalysisConfiguration());
        }
    }
}
