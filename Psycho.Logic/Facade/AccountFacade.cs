using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Psycho.DAL.Core;
using Psycho.DAL.Core.Domain;
using Psycho.Logic.Facade.Interfaces;

namespace Psycho.Logic.Facade
{
    public class AccountFacade : IAccountFacade
    {
        private IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly SignInManager<User> _signInManager;

        public AccountFacade(IUnitOfWork unitOfWork, UserManager<User> userManager, RoleManager<Role> roleManager, SignInManager<User> signInManager)
        {
            this._unitOfWork = unitOfWork;
            this._userManager = userManager;
            this._roleManager = roleManager;
            this._signInManager = signInManager;
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
    }
}
