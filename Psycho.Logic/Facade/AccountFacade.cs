using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Psycho.DAL.Core;
using Psycho.DAL.Core.Domain;
using Psycho.Logic.Facade.Interfaces;

namespace Psycho.Logic.Facade
{
    public class AccountFacade : IAccountFacade
    {
        private IUnitOfWork _unitOfWork;

        public AccountFacade(IUnitOfWork unitOfWork)
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
    }
}
