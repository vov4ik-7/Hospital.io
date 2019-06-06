using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using Psycho.Logic.Facade;
using Psycho.DAL.Persistence;
using Microsoft.AspNetCore.Identity;
using Psycho.DAL.Core.Domain;

namespace Phycho.Tests.Services
{
    public class AccountFacadeTest
    {
        [Fact]
        public void TestUnitOfWork()
        {
            var facade = GetFacade();
            var unitOfWork = facade.UnitOfWork;

            Assert.True(isNull(unitOfWork));
        }

        [Fact]
        public void TestUserManager()
        {
            var facade = GetFacade();
            var userManager = facade.UserManager;

            Assert.True(isNull(userManager));
        }

        [Fact]
        public void TestSignInManager()
        {
            var facade = GetFacade();
            var signInManager = facade.SignInManager;

            Assert.True(isNull(signInManager));
        }

        [Fact]
        public void TestRoleManager()
        {
            var facade = GetFacade();
            var roleManager = facade.RoleManager;

            Assert.True(isNull(roleManager));
        }

        public AccountFacade GetFacade()
        {
            return new AccountFacade();
        }

        public bool isNull(object o)
        {
            return o == null;
        }
    }
}
