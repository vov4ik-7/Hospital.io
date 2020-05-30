using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<IActionResult> Diary()
        {
            var userId = await GetCurrentUserId();
            var day = DateTime.Today;

            var diary = await _unitOfWork.Context.HealthDiary.Where(r => r.UserId == userId && r.Date == day).ToListAsync();
            var bodyIndicators = await _unitOfWork.Context.BodyIndicators.Where(r => r.UserId == userId && r.Date == day).ToListAsync();

            var userMoods = diary.Where(r => r.BlockName == "mood").Select(r => r.BlockValue).ToList();
            var userSymptoms = diary.Where(r => r.BlockName == "symptom").Select(r => r.BlockValue).ToList();
            var userLifestyles = diary.Where(r => r.BlockName == "lifestyle").Select(r => r.BlockValue).ToList();

            var viewModel = new DiaryViewModel
            {
                DefaultMoods = Constants.Constants.Diary.Moods,
                DefaultSymptoms = Constants.Constants.Diary.Symptoms,
                DefaultLifestyles = Constants.Constants.Diary.Lifestyles,

                UserMoods = userMoods,
                UserSymptoms = userSymptoms,
                UserLifestyles = userLifestyles,

                UserBodyIndicators = bodyIndicators
            };

            return PartialView(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Statistics()
        {
            var userId = await GetCurrentUserId();
            var end = DateTime.Today;
            var start = end.AddDays(-6);

            var bodyIndicators = await _unitOfWork.Context.BodyIndicators
                .Where(r => r.UserId == userId && r.Date >= start.Date && r.Date <= end.Date)
                .ToListAsync();

            var water = bodyIndicators.Where(i => i.Indicator == "drink-water");
            var waterSeriesData = new List<decimal>();
            var waterXaxisCategories = new List<string>();
            var weight = bodyIndicators.Where(i => i.Indicator == "weight");
            var weightSeriesData = new List<decimal>();
            var weightXaxisCategories = new List<string>();
            var sleep = bodyIndicators.Where(i => i.Indicator == "sleep");
            var sleepSeriesData = new List<decimal>();
            var sleepXaxisCategories = new List<string>();
            var temperature = bodyIndicators.Where(i => i.Indicator == "temperature");
            var temperatureSeriesData = new List<decimal>();
            var temperatureXaxisCategories = new List<string>();
            foreach (DateTime day in EachDay(start, end))
            {
                var waterElem = water.FirstOrDefault(w => w.Date.Date == day.Date);
                waterSeriesData.Add(waterElem?.Value ?? 0m);
                waterXaxisCategories.Add(day.Date.ToString("d", CultureInfo.CreateSpecificCulture("en-US")));

                var weightElem = weight.FirstOrDefault(w => w.Date.Date == day.Date);
                weightSeriesData.Add(weightElem?.Value ?? 0m);
                weightXaxisCategories.Add(day.Date.ToString("d", CultureInfo.CreateSpecificCulture("en-US")));

                var sleepElem = sleep.FirstOrDefault(w => w.Date.Date == day.Date);
                sleepSeriesData.Add(sleepElem?.Value ?? 0m);
                sleepXaxisCategories.Add(day.Date.ToString("d", CultureInfo.CreateSpecificCulture("en-US")));

                var temperatureElem = temperature.FirstOrDefault(w => w.Date.Date == day.Date);
                temperatureSeriesData.Add(temperatureElem?.Value ?? 0m);
                temperatureXaxisCategories.Add(day.Date.ToString("d", CultureInfo.CreateSpecificCulture("en-US")));
            }

            var viewModel = new StatisticsViewModel
            {
                WaterGraph = new GraphDataViewModel { SeriesData = waterSeriesData, XaxisCategories = waterXaxisCategories },
                WeightGraph = new GraphDataViewModel { SeriesData = weightSeriesData, XaxisCategories = weightXaxisCategories },
                SleepGraph = new GraphDataViewModel { SeriesData = sleepSeriesData, XaxisCategories = sleepXaxisCategories },
                TemperatureGraph = new GraphDataViewModel { SeriesData = temperatureSeriesData, XaxisCategories = temperatureXaxisCategories }
            };

            return PartialView(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddDiaryRecord(string value, string blockName, DateTime date)
        {
            var record = new HealthDiary
            {
                UserId = await GetCurrentUserId(),
                Date = date.Date,
                BlockName = blockName,
                BlockValue = value
            };

            _unitOfWork.Context.HealthDiary.Add(record);
            await _unitOfWork.Context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> RemoveDiaryRecord(string value, string blockName, DateTime date)
        {
            var userId = await GetCurrentUserId();
            var day = date.Date;
            var record = await _unitOfWork.Context.HealthDiary.FirstOrDefaultAsync(r =>
                r.UserId == userId
                && r.BlockName == blockName
                && r.BlockValue == value
                && r.Date == day);

            if(record != null)
            {
                _unitOfWork.Context.HealthDiary.Remove(record);
                await _unitOfWork.Context.SaveChangesAsync();
            }

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> AddOrUpdateBodyIndicator(decimal value, string indicator, DateTime date)
        {
            var userId = await GetCurrentUserId();
            var day = date.Date;
            var record = await _unitOfWork.Context.BodyIndicators.FirstOrDefaultAsync(r =>
                r.UserId == userId
                && r.Indicator == indicator
                && r.Date == day);

            if(record != null)
            {
                record.Value = value;
            }
            else
            {
                var newRecord = new BodyIndicator
                {
                    UserId = userId,
                    Date = day,
                    Indicator = indicator,
                    Value = value
                };

                _unitOfWork.Context.BodyIndicators.Add(newRecord);
            }

            await _unitOfWork.Context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> RemoveBodyIndicator(string indicator, DateTime date)
        {
            var userId = await GetCurrentUserId();
            var day = date.Date;
            var record = await _unitOfWork.Context.BodyIndicators.FirstOrDefaultAsync(r =>
                r.UserId == userId
                && r.Indicator == indicator
                && r.Date == day);

            if (record != null)
            {
                _unitOfWork.Context.BodyIndicators.Remove(record);
                await _unitOfWork.Context.SaveChangesAsync();
            }

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> ChangeDiaryDay(DateTime date)
        {
            var userId = await GetCurrentUserId();
            var day = date.Date;

            var diary = await _unitOfWork.Context.HealthDiary.Where(r => r.UserId == userId && r.Date == day).ToListAsync();
            var bodyIndicators = await _unitOfWork.Context.BodyIndicators.Where(r => r.UserId == userId && r.Date == day).ToListAsync();

            var userMoods = diary.Where(r => r.BlockName == "mood").Select(r => r.BlockValue).ToList();
            var userSymptoms = diary.Where(r => r.BlockName == "symptom").Select(r => r.BlockValue).ToList();
            var userLifestyles = diary.Where(r => r.BlockName == "lifestyle").Select(r => r.BlockValue).ToList();

            return new OkObjectResult(new
            {
                userMoods = userMoods,
                userSymptoms = userSymptoms,
                userLifestyles = userLifestyles,
                bodyIndicators = bodyIndicators
            });
        }

        public async Task<IActionResult> ChangeWaterGraphRange(DateTime start, DateTime end)
        {
            var userId = await GetCurrentUserId();
            start = start.Date;
            end = end.Date;

            var bodyIndicators = await _unitOfWork.Context.BodyIndicators
                .Where(r => r.UserId == userId && r.Date >= start.Date && r.Date <= end.Date)
                .ToListAsync();

            var water = bodyIndicators.Where(i => i.Indicator == "drink-water");
            var waterSeriesData = new List<decimal>();
            var waterXaxisCategories = new List<string>();
            foreach (DateTime day in EachDay(start, end))
            {
                var waterElem = water.FirstOrDefault(w => w.Date.Date == day.Date);
                waterSeriesData.Add(waterElem?.Value ?? 0m);
                waterXaxisCategories.Add(day.Date.ToString("d", CultureInfo.CreateSpecificCulture("en-US")));
            }

            var waterGraphData = new GraphDataViewModel { SeriesData = waterSeriesData, XaxisCategories = waterXaxisCategories };

            return new OkObjectResult(waterGraphData);
        }

        public async Task<IActionResult> ChangeWeightGraphRange(DateTime start, DateTime end)
        {
            var userId = await GetCurrentUserId();
            start = start.Date;
            end = end.Date;

            var bodyIndicators = await _unitOfWork.Context.BodyIndicators
                .Where(r => r.UserId == userId && r.Date >= start.Date && r.Date <= end.Date)
                .ToListAsync();

            var weight = bodyIndicators.Where(i => i.Indicator == "weight");
            var weightSeriesData = new List<decimal>();
            var weightXaxisCategories = new List<string>();
            
            foreach (DateTime day in EachDay(start, end))
            {
                var weightElem = weight.FirstOrDefault(w => w.Date.Date == day.Date);
                weightSeriesData.Add(weightElem?.Value ?? 0m);
                weightXaxisCategories.Add(day.Date.ToString("d", CultureInfo.CreateSpecificCulture("en-US")));
            }

            var weightGraphData = new GraphDataViewModel { SeriesData = weightSeriesData, XaxisCategories = weightXaxisCategories };

            return new OkObjectResult(weightGraphData);
        }

        public async Task<IActionResult> ChangeSleepGraphRange(DateTime start, DateTime end)
        {
            var userId = await GetCurrentUserId();
            start = start.Date;
            end = end.Date;

            var bodyIndicators = await _unitOfWork.Context.BodyIndicators
                .Where(r => r.UserId == userId && r.Date >= start.Date && r.Date <= end.Date)
                .ToListAsync();

            var sleep = bodyIndicators.Where(i => i.Indicator == "sleep");
            var sleepSeriesData = new List<decimal>();
            var sleepXaxisCategories = new List<string>();
            
            foreach (DateTime day in EachDay(start, end))
            {
                var sleepElem = sleep.FirstOrDefault(w => w.Date.Date == day.Date);
                sleepSeriesData.Add(sleepElem?.Value ?? 0m);
                sleepXaxisCategories.Add(day.Date.ToString("d", CultureInfo.CreateSpecificCulture("en-US")));
            }

            var sleepGraphData = new GraphDataViewModel { SeriesData = sleepSeriesData, XaxisCategories = sleepXaxisCategories };

            return new OkObjectResult(sleepGraphData);
        }

        public async Task<IActionResult> ChangeTemperatureGraphRange(DateTime start, DateTime end)
        {
            var userId = await GetCurrentUserId();
            start = start.Date;
            end = end.Date;

            var bodyIndicators = await _unitOfWork.Context.BodyIndicators
                .Where(r => r.UserId == userId && r.Date >= start.Date && r.Date <= end.Date)
                .ToListAsync();

            var temperature = bodyIndicators.Where(i => i.Indicator == "temperature");
            var temperatureSeriesData = new List<decimal>();
            var temperatureXaxisCategories = new List<string>();
            foreach (DateTime day in EachDay(start, end))
            {
                var temperatureElem = temperature.FirstOrDefault(w => w.Date.Date == day.Date);
                temperatureSeriesData.Add(temperatureElem?.Value ?? 0m);
                temperatureXaxisCategories.Add(day.Date.ToString("d", CultureInfo.CreateSpecificCulture("en-US")));
            }

            var temperatureGraphData = new GraphDataViewModel { SeriesData = temperatureSeriesData, XaxisCategories = temperatureXaxisCategories };

            return new OkObjectResult(temperatureGraphData);
        }

        [HttpGet]
        public async Task<int> GetCurrentUserId()
        {
            User usr = await GetCurrentUserAsync();
            return usr.Id;
        }

        private Task<User> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        private IEnumerable<DateTime> EachDay(DateTime from, DateTime thru)
        {
            for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
                yield return day;
        }
    }
}
