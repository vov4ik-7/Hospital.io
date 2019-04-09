using System;
using Psycho.DAL.Core.Repositories;

namespace Psycho.DAL.Core
{
    public interface IUnitOfWork : IDisposable
    {
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
