using System;
using System.Collections.Generic;
using Psycho.DAL.Core.Domain;

namespace Psycho.io.Models.User
{
    public class FullAppointmentResultViewModel
    {
        public int Id { get; set; }
        public Psychologist Doctor { get; set; }
        public AuthorizedUser Patient { get; set; }
        public DateTime? Date { get; set; }
        public List<AnalysisViewModel> Analyses { get; set; }
        public string Status { get; set; }
    }
}
