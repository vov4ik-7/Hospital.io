using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Psycho.DAL.Core;
using Psycho.DAL.Core.Domain;
using Psycho.DTO.Core;
using Psycho.DTO.Persistence;
using Psycho.Logic.DataMappers;
using Psycho.Logic.Facade.Interfaces;

namespace Psycho.Logic.Facade
{
    public class AdminFacade : IAdminFacade
    {
        private IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly SignInManager<User> _signInManager;
        private PsychologistMapper psychologistMapper;

        public AdminFacade(IUnitOfWork unitOfWork, UserManager<User> userManager, RoleManager<Role> roleManager, SignInManager<User> signInManager)
        {
            this._unitOfWork = unitOfWork;
            this._userManager = userManager;
            this._roleManager = roleManager;
            this._signInManager = signInManager;
            psychologistMapper = new PsychologistMapper();
        }

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

        public RoleManager<Role> RoleManager
        {
            get
            {
                return this._roleManager;
            }
        }

        public SignInManager<User> SignInManager
        {
            get
            {
                return this._signInManager;
            }
        }

        public PsychologistListDTO GetPsychologistListForAdminPage()
        {
            List<Psychologist> psychologists = _unitOfWork.Psychologists.GetAll().ToList();
            PsychologistListDTO psychologistListDTO = psychologistMapper.Map(psychologists);

            return psychologistListDTO;
        }

        public async Task<Tuple<string, string>> CreatePsychologistAsync(CreatePsychologistDTO newPsychologist)
        {
            string status = String.Empty;
            string description = String.Empty;

            var check = await this._userManager.FindByEmailAsync(newPsychologist.Email);

            if(check == null)
            {
                Gender? gender = newPsychologist.Gender != String.Empty ? (Gender?)Enum.Parse(typeof(Gender), newPsychologist.Gender) : null;
                string[] Date = newPsychologist.HireDate.Split('/', StringSplitOptions.RemoveEmptyEntries);
                User user = new Psychologist()
                {
                    UserName = newPsychologist.Email,
                    FirstName = newPsychologist.FirstName,
                    LastName = newPsychologist.LastName,
                    Email = newPsychologist.Email,
                    EmailConfirmed = true,
                    Blocked = false,
                    RoleId = 2,
                    Gender = gender,
                    Address = newPsychologist.Address,
                    PhoneNumber = newPsychologist.Phone,
                    Position = "Psychologist",
                    HireDate = new DateTime(Int32.Parse(Date[2]), Int32.Parse(Date[0]), Int32.Parse(Date[1]))
                };

                var result = await _userManager.CreateAsync(user, newPsychologist.Password);
                if (result.Succeeded)
                {
                    status = "success";
                    description = "User created successfully";
                }
                else
                {
                    status = "error";
                    description = "Server error.";
                }
            }
            else
            {
                status = "error";
                description = "User with this email already exists.";
            }
            return new Tuple<string, string>(status, description);
        }

        public async Task<CreatePsychologistDTO> GetPsychologistForEditAsync(int id)
        {
            Psychologist psychologist = (Psychologist)await _userManager.FindByIdAsync(id.ToString());

            CreatePsychologistDTO forEdit = new CreatePsychologistDTO
            {
                Id = psychologist.Id,
                Email = psychologist.Email,
                FirstName = psychologist.FirstName,
                LastName = psychologist.LastName,
                Address = psychologist.Address,
                Gender = psychologist.Gender.ToString(),
                Phone = psychologist.PhoneNumber,
                Position = psychologist.Position,
                HireDate = $"{psychologist.HireDate.Month}/{psychologist.HireDate.Day}/{psychologist.HireDate.Year}",
                Password = ""
            };

            return forEdit;
        }

        public async Task<Tuple<string, string>> EditPsychologistAsync(CreatePsychologistDTO psychologistDTO)
        {
            string status = String.Empty;
            string description = String.Empty;

            Psychologist psychologist = (Psychologist)await this._userManager.FindByIdAsync(psychologistDTO.Id.ToString());
            var check = await this._userManager.FindByEmailAsync(psychologistDTO.Email);

            if (check == null || psychologist.Id == check.Id)
            {
                psychologist.FirstName = psychologistDTO.FirstName;
                psychologist.LastName = psychologistDTO.LastName;
                psychologist.Email = psychologistDTO.Email;
                psychologist.UserName = psychologistDTO.Email;
                psychologist.Address = psychologistDTO.Address;
                psychologist.PhoneNumber = psychologistDTO.Phone;
                psychologist.Gender = psychologistDTO.Gender != String.Empty ? (Gender?)Enum.Parse(typeof(Gender), psychologistDTO.Gender) : null;

                if (psychologistDTO.Password != "")
                {

                }

                var result = await this._userManager.UpdateAsync(psychologist);
                if (result.Succeeded)
                {
                    status = "success";
                    description = "User updated successfully";
                }
                else
                {
                    status = "error";
                    description = "Server error.";
                }
            }
            else
            {
                status = "error";
                description = "User with this email already exists.";
            }
            return new Tuple<string, string>(status, description);
        }

        public async Task<DeletePsychologistDTO> GetPsychologistForDeleteAsync(int id)
        {
            Psychologist psychologist = (Psychologist)await _userManager.FindByIdAsync(id.ToString());

            DeletePsychologistDTO forDelete = new DeletePsychologistDTO
            {
                Id = psychologist.Id,
                Name = $"{psychologist.FirstName} {psychologist.LastName}"
            };

            return forDelete;
        }

        public async Task<Tuple<string, string>> DeletePsychologistAsync(int id)
        {
            string status = String.Empty;
            string description = String.Empty;

            var check = await this._userManager.FindByIdAsync(id.ToString());

            if (check != null)
            {
                var result = await this._userManager.DeleteAsync(check);

                if (result.Succeeded)
                {
                    status = "success";
                    description = "User deleted successfully";
                }
                else
                {
                    status = "error";
                    description = "Server error.";
                }
            }
            else
            {
                status = "error";
                description = "There are no such user in database.";
            }
            return new Tuple<string, string>(status, description);
        }
    }
}
