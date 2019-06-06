using Psycho.Logic.Facade;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Phycho.Tests.Controllers
{
    public class HomeControllerTest
    {
        [Fact]
        public void FacebookAuthTest()
        {
            var facade = GetFacade();
            var unitOfWork = facade.UnitOfWork;

            Assert.True(isNull(unitOfWork));
        }

        [Fact]
        public void AboutTest()
        {
            var facade = GetFacade();
            var userManager = facade.UserManager;

            Assert.True(isNull(userManager));
        }

        [Fact]
        public void ContactTest()
        {
            var facade = GetFacade();
            var signInManager = facade.SignInManager;

            Assert.True(isNull(signInManager));
        }

        [Fact]
        public void PrivacyTest()
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
