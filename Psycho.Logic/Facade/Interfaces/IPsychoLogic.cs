using System;
namespace Psycho.Logic.Facade.Interfaces
{
    public interface IPsychoLogic
    {
        IAccountFacade AccountFacade { get; }
        IAdminFacade AdminFacade { get; }
        IUserFacade UserFacade { get; }
    }
}
