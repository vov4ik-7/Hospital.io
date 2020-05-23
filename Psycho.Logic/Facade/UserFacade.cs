using System;
using System.Linq;
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

        public User GetUser(int id)
        {
            User user = _unitOfWork.Users.Get(id);
            return user;
        }

        public UserDTO GetUserDTO(int id)
        {
            User user = this.GetUser(id);
            var workSchedule = (user as Psychologist).WorkSchedules.ToList();
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
                Weight = (user as AuthorizedUser)?.Weight,

                MondayStart = workSchedule.FirstOrDefault(s => s.Day == Day.Monday)?.StartTime,
                MondayEnd = workSchedule.FirstOrDefault(s => s.Day == Day.Monday)?.EndTime,
                TuesdayStart = workSchedule.FirstOrDefault(s => s.Day == Day.Tuesday)?.StartTime,
                TuesdayEnd = workSchedule.FirstOrDefault(s => s.Day == Day.Tuesday)?.EndTime,
                WednesdayStart = workSchedule.FirstOrDefault(s => s.Day == Day.Wednesday)?.StartTime,
                WednesdayEnd = workSchedule.FirstOrDefault(s => s.Day == Day.Wednesday)?.EndTime,
                ThursdayStart = workSchedule.FirstOrDefault(s => s.Day == Day.Thursday)?.StartTime,
                ThursdayEnd = workSchedule.FirstOrDefault(s => s.Day == Day.Thursday)?.EndTime,
                FridayStart = workSchedule.FirstOrDefault(s => s.Day == Day.Friday)?.StartTime,
                FridayEnd = workSchedule.FirstOrDefault(s => s.Day == Day.Friday)?.EndTime,
                SaturdayStart = workSchedule.FirstOrDefault(s => s.Day == Day.Saturday)?.StartTime,
                SaturdayEnd = workSchedule.FirstOrDefault(s => s.Day == Day.Saturday)?.EndTime,
                SundayStart = workSchedule.FirstOrDefault(s => s.Day == Day.Sunday)?.StartTime,
                SundayEnd = workSchedule.FirstOrDefault(s => s.Day == Day.Sunday)?.EndTime,
            };

            return userDTO;
        }
    }
}
