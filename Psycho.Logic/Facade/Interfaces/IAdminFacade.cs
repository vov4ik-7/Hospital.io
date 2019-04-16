using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Psycho.DTO.Core;
using Psycho.DTO.Persistence;

namespace Psycho.Logic.Facade.Interfaces
{
    public interface IAdminFacade
    {
        PsychologistListDTO GetPsychologistListForAdminPage();
        Task<Tuple<string, string>> CreatePsychologistAsync(CreatePsychologistDTO newPsychologist);
    }
}
