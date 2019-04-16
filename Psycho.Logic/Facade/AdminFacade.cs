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
                    HireDate = newPsychologist.HireDate
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
    }
}
