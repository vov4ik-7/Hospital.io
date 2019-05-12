using System;
using System.Collections.Generic;
using Psycho.DAL.Core.Domain;
using Psycho.DTO.Core;

namespace Psycho.DTO.Persistence
{
    public class PsychologistWithAppointmentsDTO
    {
        public string Name { get; set; }

        public Psychologist Psychologist { get; set; }

        public List<AppointmentForCalendarDTO> appointments { get; set; } = new List<AppointmentForCalendarDTO>();

        public string json { get; set; }
    }
}
