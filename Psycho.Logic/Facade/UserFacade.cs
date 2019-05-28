using System;
using Microsoft.AspNetCore.Identity;
using Psycho.DAL.Core;
using Psycho.DAL.Core.Domain;
using Psycho.DTO.Core;
using Psycho.Logic.Facade.Interfaces;

namespace Psycho.Logic.Facade
{
    public class UserFacade : IUserFacade
    {
        private IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;

        public UserFacade(IUnitOfWork unitOfWork, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public UserFacade()
        { }

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return this._unitOfWork;
            }
        }

        public UserManager<User> UserManager
        {
            get
            {
                return this._userManager;
            }
        }

        public User GetUser(int id)
        {
            User user = _unitOfWork.Users.Get(id);
            return user;
        }

        public UserDTO GetUserDTO(int id)
        {
            User user = this.GetUser(id);
            UserDTO userDTO = new UserDTO
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Phone = user.PhoneNumber,
                Address = user.Address,
                Gender = user.Gender?.ToString(),

                Position = (user as Psychologist)?.Position,
                HireDate = (user as Psychologist)?.HireDate,

                Age = (user as AuthorizedUser)?.Age,
                Height = (user as AuthorizedUser)?.Height,
                Weight = (user as AuthorizedUser)?.Weight
            };

            return userDTO;
        }
    }
}
