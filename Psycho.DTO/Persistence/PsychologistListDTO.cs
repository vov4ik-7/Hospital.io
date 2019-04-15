using System;
using System.Collections.Generic;
using Psycho.DTO.Core;

namespace Psycho.DTO.Persistence
{
    public class PsychologistListDTO
    {
        public List<string> ColumnHeaders { get; set; } = new List<string>
        {
            "Name", "Email", "Phone", "Position", "Start date", "Actions"
        };
        public List<PsychologistDTO> PsychologistDTOs { get; set; } = new List<PsychologistDTO>();

        public PsychologistListDTO()
        {
        }
    }
}
