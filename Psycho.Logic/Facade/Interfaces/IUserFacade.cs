using System;
using Psycho.DAL.Core.Domain;
using Psycho.DTO.Core;

namespace Psycho.Logic.Facade.Interfaces
{
    public interface IUserFacade
    {
        User GetUser(int id);
        UserDTO GetUserDTO(int id);
    }
}
