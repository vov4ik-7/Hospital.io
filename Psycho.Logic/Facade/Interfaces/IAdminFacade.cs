using System;
using System.Collections.Generic;
using Psycho.DTO.Persistence;

namespace Psycho.Logic.Facade.Interfaces
{
    public interface IAdminFacade
    {
        PsychologistListDTO GetPsychologistListForAdminPage();
    }
}
