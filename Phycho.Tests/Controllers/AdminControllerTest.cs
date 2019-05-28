using Psycho.Logic.Facade;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Phycho.Tests.Controllers
{
    public class AdminControllerTest
    {
        [Fact]
        public void TestIndex()
        {
            var facade = GetFacade();
            var unitOfWork = facade.UnitOfWork;

            Assert.True(isNull(unitOfWork));
        }

        [Fact]
        public void TestCreatePsychologist()
        {
            var facade = GetFacade();
            var userManager = facade.UserManager;

            Assert.True(isNull(userManager));
        }

        [Fact]
        public void TestEditPsychologist()
        {
            var facade = GetFacade();
            var signInManager = facade.SignInManager;

            Assert.True(isNull(signInManager));
        }

        [Fact]
        public void TestDeletePsychologist()
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
