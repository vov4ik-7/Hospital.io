using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Psycho.DAL.Core.Domain;
using Psycho.DTO.Core;
using Psycho.DTO.Persistence;
using Psycho.Logic.Facade.Interfaces;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Psycho.io.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : PsychoMvcControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly SignInManager<User> _signInManager;

        public AdminController(IPsychoLogic psychoLogic,
            UserManager<User> userManager,
            RoleManager<Role> roleManager,
            SignInManager<User> signInManager)
            : base(psychoLogic)
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
            this._signInManager = signInManager;
        }

        public IActionResult Index()
        {
            PsychologistListDTO psychologistListDTO = PsychoLogic.AdminFacade.GetPsychologistListForAdminPage();

            return View(psychologistListDTO);
        }

        [HttpGet]
        public IActionResult CreatePsychologist(string returnUrl = null)
        {
            return PartialView(new CreatePsychologistDTO { ReturnUrl = returnUrl });
        }

        [HttpPost]
        public async Task<IActionResult> CreatePsychologist(CreatePsychologistDTO model)
        {
            Tuple<string, string> tuple = await this.PsychoLogic.AdminFacade.CreatePsychologistAsync(model);
            return Json(new { status = tuple.Item1, description = tuple.Item2 });
        }

        [HttpGet]
        public async Task<IActionResult> EditPsychologist(int id)
        {
            CreatePsychologistDTO psychologistDTO = await PsychoLogic.AdminFacade.GetPsychologistForEditAsync(id);
            return PartialView(psychologistDTO);
        }

        [HttpPost]
        public async Task<IActionResult> EditPsychologist(CreatePsychologistDTO model)
        {
            Tuple<string, string> tuple = await this.PsychoLogic.AdminFacade.EditPsychologistAsync(model);
            return Json(new { status = tuple.Item1, description = tuple.Item2 });
        }


        [HttpGet]
        public async Task<IActionResult> DeletePsychologistModal(int id)
        {
            DeletePsychologistDTO psychologistDTO = await PsychoLogic.AdminFacade.GetPsychologistForDeleteAsync(id);
            return PartialView(psychologistDTO);
        }

        [HttpPost]
        public async Task<IActionResult> DeletePsychologist(int id)
        {
            Tuple<string, string> tuple = await this.PsychoLogic.AdminFacade.DeletePsychologistAsync(id);
            return Json(new { status = tuple.Item1, description = tuple.Item2 });
        }
    }
}
