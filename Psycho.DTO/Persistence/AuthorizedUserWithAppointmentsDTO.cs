using System;
using System.Collections.Generic;
using Psycho.DAL.Core.Domain;
using Psycho.DTO.Core;

namespace Psycho.DTO.Persistence
{
    public class AuthorizedUserWithAppointmentsDTO
    {
        public string Name { get; set; }

        public AuthorizedUser AuthorizedUser { get; set; }

        public List<AppointmentForCalendarDTO> appointments { get; set; } = new List<AppointmentForCalendarDTO>();

        public string json { get; set; }
    }
}
