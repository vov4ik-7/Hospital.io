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
        Task<CreatePsychologistDTO> GetPsychologistForEditAsync(int id);
        Task<Tuple<string, string>> EditPsychologistAsync(CreatePsychologistDTO psychologistDTO);
        Task<DeletePsychologistDTO> GetPsychologistForDeleteAsync(int id);
        Task<Tuple<string, string>> DeletePsychologistAsync(int id);
    }
}
