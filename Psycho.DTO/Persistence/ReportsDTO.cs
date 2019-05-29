using System;
using System.Collections.Generic;
using Psycho.DAL.Core.Domain;
using Psycho.DTO.Core;

namespace Psycho.DTO.Persistence
{
    public class ReportsDTO
    {
        public Psychologist Psychologist { get; set; } = new Psychologist();

        public List<ReportDTO> Reports { get; set; } = new List<ReportDTO>();
    }
}
