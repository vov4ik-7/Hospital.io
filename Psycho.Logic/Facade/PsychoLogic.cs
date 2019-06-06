using System;
using Microsoft.AspNetCore.Identity;
using Psycho.DAL.Core;
using Psycho.DAL.Core.Domain;
using Psycho.Logic.Facade.Interfaces;

namespace Psycho.Logic.Facade
{
    public class PsychoLogic : IPsychoLogic
    {
        private IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly SignInManager<User> _signInManager;
        private IAccountFacade _accountFacade;
        private IAdminFacade _adminFacade;
        private IUserFacade _userFacade;

        public PsychoLogic(IUnitOfWork unitOfWork, UserManager<User> userManager, RoleManager<Role> roleManager, SignInManager<User> signInManager)
        {
            this._unitOfWork = unitOfWork;
            this._userManager = userManager;
            this._roleManager = roleManager;
            this._signInManager = signInManager;
        }

        public PsychoLogic()
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

        public IAccountFacade AccountFacade
        {
            get
            {
                if(this._accountFacade == null)
                {
                    this._accountFacade = new AccountFacade(this._unitOfWork, this._userManager, this._roleManager, this._signInManager);
                }
                return this._accountFacade;
            }
        }

        public IAdminFacade AdminFacade
        {
            get
            {
                if (this._adminFacade == null)
                {
                    this._adminFacade = new AdminFacade(this._unitOfWork, this._userManager, this._roleManager, this._signInManager);
                }
                return this._adminFacade;
            }
        }

        public IUserFacade UserFacade
        {
            get
            {
                if(_userFacade == null)
                {
                    _userFacade = new UserFacade(_unitOfWork, _userManager);
                }
                return _userFacade;
            }
        }
    }
}
