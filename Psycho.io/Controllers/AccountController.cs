using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Psycho.Logic.Facade.Interfaces;
using Psycho.io.ViewModels;
using Microsoft.AspNetCore.Identity;
using Psycho.DAL.Core.Domain;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Psycho.io.Controllers
{
    public class AccountController : PsychoMvcControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(IPsychoLogic psychoLogic,
            UserManager<User> userManager,
            RoleManager<Role> roleManager,
            SignInManager<User> signInManager)
            : base(psychoLogic)
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
            this._signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Signup(SignupViewModel model)
        {
            if (ModelState.IsValid)
            {
                Gender? gender = model.Gender != String.Empty ? (Gender?)Enum.Parse(typeof(Gender), model.Gender) : null;
                User user = new AuthorizedUser() { UserName = model.Email, FirstName = model.FirstName, LastName = model.LastName, Email = model.Email,
                                                   EmailConfirmed = true, Blocked = false,
                                                   RoleId = 3, Gender = gender, Age = model.Age,
                                                   Height = model.Height, Weight = model.Weight, PhoneNumber = model.Phone };

                var result = await _userManager.CreateAsync(user, model.Password);
                var add_role = await _userManager.AddToRoleAsync(user, "AuthorizedUser");
                User signinUser = await _userManager.FindByEmailAsync(model.Email);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(signinUser, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Signin(string returnUrl = null)
        {
            return PartialView(new SigninViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Signin(SigninViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result =
                    await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Incorrect email and (or) password.");
                }
            }
            return View(model);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Signout()
        {
            await _signInManager.SignOutAsync();
            return Json(Url.Action("Index", "Home"));//RedirectToAction("Index", "Home");
        }
    }
}
