using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Psycho.Logic.Facade.Interfaces;
using Psycho.io.ViewModels;
using Microsoft.AspNetCore.Identity;
using Psycho.DAL.Core.Domain;
using System.Net.Http;
using Newtonsoft.Json;
using static Psycho.io.Models.Facebook.FacebookToken;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Psycho.io.Controllers
{
    public class AccountController : PsychoMvcControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly SignInManager<User> _signInManager;
        private static readonly HttpClient Client = new HttpClient();

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
        [Route("user/facebook")]
        public async Task<ActionResult> FacebookPost(string accessToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var appAccessTokenResponse = await Client.GetStringAsync($"https://graph.facebook.com/oauth/access_token?client_id={Facebook.AppId}&client_secret={Facebook.AppSecret}&grant_type=client_credentials");
                var appAccessToken = JsonConvert.DeserializeObject<FacebookAppAccessToken>(appAccessTokenResponse);

                var userAccessTokenValidationResponse = await Client.GetStringAsync($"https://graph.facebook.com/debug_token?input_token={accessToken}&access_token={appAccessToken.AccessToken}");
                var userAccessTokenValidation = JsonConvert.DeserializeObject<FacebookUserAccessTokenValidation>(userAccessTokenValidationResponse);

                if (!userAccessTokenValidation.Data.IsValid)
                {
                    return BadRequest("Invalid facebook token!");
                }

                var userInfoResponse = await Client.GetStringAsync($"https://graph.facebook.com/v3.2/me?fields=id,email,first_name,last_name,name,picture&access_token={accessToken}");
                var userInfo = JsonConvert.DeserializeObject<FacebookUserData>(userInfoResponse);

                var user = await _userManager.FindByEmailAsync(userInfo.Email);

                if (user == null)
                {
                    var user_role = await _roleManager.FindByNameAsync("AuthorizedUser");
                    User new_user = new AuthorizedUser
                    {
                        FirstName = userInfo.FirstName,
                        LastName = userInfo.LastName,
                        Email = userInfo.Email,
                        UserName = userInfo.Email,
                        Role = user_role,
                        RoleId = user_role.Id,
                        EmailConfirmed = true,
                        Blocked = false
                    };
                    var result = await _userManager.CreateAsync(new_user, Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 8));
                    var new_user_from_db = await _userManager.FindByEmailAsync(new_user.Email);
                    var add_role = await _userManager.AddToRoleAsync(new_user_from_db, "AuthorizedUser");

                    if (!result.Succeeded)
                        return BadRequest();
                }

                var localUser = await _userManager.FindByNameAsync(userInfo.Email);

                if (localUser == null)
                {
                    return BadRequest("Failed to create local user account.");
                }

                await _signInManager.SignInAsync(localUser, false);

                return RedirectToAction("Index", "Home");

                /*var userDto = new UserIdentityDTO()
                {
                    Email = localUser.Email,
                    LastName = localUser.LastName,
                    FirstName = localUser.FirstName,
                    Id = localUser.Id,
                    Role = localUser.Role.Name,
                    EmailConfirmed = localUser.EmailConfirmed,
                    Blocked = localUser.Blocked,
                    Password = localUser.Password
                };

                string jwt = JwtManager.GenerateToken(userDto);
                return new JsonResult(jwt);*/
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Signout()
        {
            await _signInManager.SignOutAsync();
            return Json(Url.Action("Index", "Home"));//RedirectToAction("Index", "Home");
        }
    }

    public static class Facebook
    {
        public const string AppId = "319767072025131";
        public const string AppSecret = "b86f169a975659ef84fd784b18d6af25";
    }
}
