using System;
using Psycho.DAL.Core;
using Psycho.Logic.Facade.Interfaces;

namespace Psycho.Logic.Facade
{
    public class PsychoLogic : IPsychoLogic
    {
        private IUnitOfWork _unitOfWork;
        private IAccountFacade _accountFacade;
        private IAdminFacade _adminFacade;

        public PsychoLogic(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return this._unitOfWork;
            }
        }

        public IAccountFacade AccountFacade
        {
            get
            {
                if(this._accountFacade == null)
                {
                    this._accountFacade = new AccountFacade(this._unitOfWork);
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
                    this._adminFacade = new AdminFacade(this._unitOfWork);
                }
                return this._adminFacade;
            }
        }
    }
}
