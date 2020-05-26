using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Psycho.DAL.Core;
using Psycho.DAL.Core.Domain;
using Psycho.io.Models.Diary;
using Psycho.Logic.Facade.Interfaces;


namespace Psycho.io.Controllers
{
    [Authorize(Roles = "AuthorizedUser")]
    public class DiaryController : PsychoMvcControllerBase
    {
        private UserManager<User> _userManager;
        private IUnitOfWork _unitOfWork;

        public DiaryController(IUnitOfWork unitOfWork,
            IPsychoLogic psychoLogic,
            UserManager<User> userManager) : base(psychoLogic)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult HealthDiary()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Diary()
        {
            var viewModel = new DiaryViewModel {
                DefaultMoods = Constants.Constants.Diary.Moods,
                DefaultSymptoms = Constants.Constants.Diary.Symptoms,
                DefaultLifestyles = Constants.Constants.Diary.Lifestyles
            };
            return PartialView(viewModel);
        }

        [HttpGet]
        public IActionResult Statistics()
        {
            return PartialView();
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
