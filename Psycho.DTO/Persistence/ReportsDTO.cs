using System;
using System.Collections.Generic;
using Psycho.DAL.Core.Domain;

namespace Psycho.DTO.Persistence
{
    public class ReportsDTO
    {
        public Psychologist Psychologist { get; set; } = new Psychologist();

        public List<Report> Reports { get; set; } = new List<Report>();
    }
}
