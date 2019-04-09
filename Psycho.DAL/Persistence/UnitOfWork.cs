using Psycho.DAL.Core;
using Psycho.DAL.Core.Repositories;
using Psycho.DAL.Persistence.Repositories;

namespace Psycho.DAL.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PsychoContext _context;

        public UnitOfWork(PsychoContext context)
        {
            _context = context;
            Roles = new RoleRepository(_context);
            Users = new UserRepository(_context);
            Psychologists = new PsychologistRepository(_context);
            AuthorizedUsers = new AuthorizedUserRepository(_context);
            AnonymousUsers = new AnonymousUserRepository(_context);
            Appointments = new AppointmentRepository(_context);
            AppointmentResults = new AppointmentResultRepository(_context);
            Reports = new ReportRepository(_context);
            Chats = new ChatRepository(_context);
            WorkSchedules = new WorkScheduleRepository(_context);
        }

        public IRoleRepository Roles { get; private set; }
        public IUserRepository Users { get; private set; }
        public IPsychologistRepository Psychologists { get; private set; }
        public IAuthorizedUserRepository AuthorizedUsers { get; private set; }
        public IAnonymousUserRepository AnonymousUsers { get; private set; }
        public IAppointmentRepository Appointments { get; private set; }
        public IAppointmentResultRepository AppointmentResults { get; private set; }
        public IReportRepository Reports { get; private set; }
        public IChatRepository Chats { get; private set; }
        public IWorkScheduleRepository WorkSchedules { get; private set; }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
