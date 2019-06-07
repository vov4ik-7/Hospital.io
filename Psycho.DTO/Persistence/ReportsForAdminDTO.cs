using System;
using System.Collections.Generic;
using Psycho.DTO.Core;

namespace Psycho.DTO.Persistence
{
    public class ReportsForAdminDTO
    {
        public List<ReportDTO> reports { get; set; } = new List<ReportDTO>();

        public List<string> ColumnHeaders { get; set; } = new List<string>
        {
            "Psychologist", "Authorized user", "Message", "Action"
        };

    }
}
