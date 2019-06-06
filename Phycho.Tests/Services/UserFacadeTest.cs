using Psycho.Logic.Facade;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Phycho.Tests.Services
{
    public class UserFacadeTest
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

        public UserFacade GetFacade()
        {
            return new UserFacade();
        }

        public bool isNull(object o)
        {
            return o == null;
        }
    }
}
