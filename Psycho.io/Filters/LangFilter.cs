using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Psycho.io.Models;

namespace Psycho.io.Filters
{
    public class LangFilter : IActionFilter
    {
        private const string CookieLangKey = "lang";

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Controller is Controller controller && (context.Result is ViewResult || context.Result is PartialViewResult))
            {
                var selectedLang = context.HttpContext.Request.Cookies.ContainsKey(CookieLangKey)
                    ? context.HttpContext.Request.Cookies[CookieLangKey]
                    : "en";
                
                var langFilePath = Path.Combine(Environment.CurrentDirectory, "App_Data", "lang", $"{selectedLang}.json");
                var langContent = File.ReadAllText(langFilePath);
                controller.ViewData["lang"] = JsonConvert.DeserializeObject<HospitalContent>(langContent);
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
        }
    }
}