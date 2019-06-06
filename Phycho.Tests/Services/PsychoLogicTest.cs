using Psycho.Logic.Facade;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Phycho.Tests.Services
{
    public class PsychoLogicTest
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

        public PsychoLogic GetFacade()
        {
            return new PsychoLogic();
        }

        public bool isNull(object o)
        {
            return o == null;
        }
    }
}
