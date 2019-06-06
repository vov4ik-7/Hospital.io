using System;
using Psycho.DAL.Core.Repositories;
using Psycho.DAL.Persistence;

namespace Psycho.DAL.Core
{
    public interface IUnitOfWork : IDisposable
    {
        PsychoContext Context { get; }
        IRoleRepository Roles { get; }
        IUserRepository Users { get; }
        IPsychologistRepository Psychologists { get; }
        IAuthorizedUserRepository AuthorizedUsers { get; }
        IAnonymousUserRepository AnonymousUsers { get; }
        IAppointmentRepository Appointments { get; }
        IAppointmentResultRepository AppointmentResults { get; }
        IReportRepository Reports { get; }
        IChatRepository Chats { get; }
        IWorkScheduleRepository WorkSchedules { get; }

        int Complete();
    }
}
