using System;
using System.Collections.Generic;
using Psycho.DAL.Core.Domain;

namespace Psycho.DTO.Core
{
    public class CreateAppointmentDTO
    {
        public int PsychologistId { get; set; }
        public int AuthorizedUserId { get; set; }
        public DateTime? StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }
        public string Info { get; set; }
        public Psychologist CurrentPsychologist { get; set; } = new Psychologist();
        public AuthorizedUser CurrentAuthorizedUser { get; set; } = new AuthorizedUser();

        public List<Psychologist> Psychologists { get; set; } = new List<Psychologist>();
        public List<AuthorizedUser> AuthorizedUsers { get; set; } = new List<AuthorizedUser>();
    }
}
