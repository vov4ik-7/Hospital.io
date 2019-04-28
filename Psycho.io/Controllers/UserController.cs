using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Psycho.DAL.Core.Domain;
using Psycho.DTO.Core;
using Psycho.Logic.Facade.Interfaces;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Psycho.io.Controllers
{
    [Authorize(Roles = "Admin,AuthorizedUser")]
    public class UserController : PsychoMvcControllerBase
    {
        private UserManager<User> _userManager;

        public UserController(IPsychoLogic psychoLogic, UserManager<User> userManager) : base(psychoLogic) {
            _userManager = userManager;
        }

        public async Task<IActionResult> Profile()
        {
            var user = await GetCurrentUserAsync();
            var userId = user.Id;
            UserDTO userDTO = PsychoLogic.UserFacade.GetUserDTO(userId);
            return View(userDTO);
        }

        [HttpGet]
        public async Task<int> GetCurrentUserId()
        {
            User usr = await GetCurrentUserAsync();
            return usr.Id;
        }

        private Task<User> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
    }
}
