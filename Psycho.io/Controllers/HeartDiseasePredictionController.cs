using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Psycho.DAL.Core;
using Psycho.DAL.Core.Domain;
using Psycho.io.Mappers;
using Psycho.io.Models.HeartDiseasePrediction;
using Psycho.Logic.Models.HeartDiseasePrediction;
using Psycho.Logic.Services.Interfaces;

namespace Psycho.io.Controllers
{
    public class HeartDiseasePredictionController : Controller
    {
        private readonly IHeartDiseasePredictionService _heartDiseasePredictionService;
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly HeartDataMapper _heartDataMapper = new HeartDataMapper();
        
        public HeartDiseasePredictionController(IHeartDiseasePredictionService heartDiseasePredictionService,
            IUnitOfWork unitOfWork,
            UserManager<User> userManager)
        {
            _heartDiseasePredictionService = heartDiseasePredictionService;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }
        
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Predict(HeartDataViewModel model)
        {
            var heartData = _heartDataMapper.Map(model);
            var result = _heartDiseasePredictionService.Predict(heartData);
            if (User.IsInRole("AuthorizedUser") || User.IsInRole("AnonymousUser"))
            {
                var userId = await GetCurrentUserId();
            }
            
            return Json(new { prediction = result.Prediction, probability = result.Probability, score = result.Score });
        }
        
        private async Task<int> GetCurrentUserId()
        {
            User usr = await GetCurrentUserAsync();
            return usr.Id;
        }

        private Task<User> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
    }
}
