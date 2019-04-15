using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Psycho.Logic.Facade;
using Psycho.Logic.Facade.Interfaces;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Psycho.io.Controllers
{
    public class PsychoMvcControllerBase : Controller
    {
        private readonly IPsychoLogic _psychoLogic;

        public IPsychoLogic PsychoLogic
        {
            get
            {
                return this._psychoLogic;
            }
        }

        public PsychoMvcControllerBase (IPsychoLogic psychoLogic)
        {
            this._psychoLogic = psychoLogic;
        }
    }
}
