using System;
using Psycho.DTO.Persistence;

namespace Psycho.io.Models.Doctor
{
    public class DoctorsViewModel
    {
        public PsychologistListDTO Doctors { get; set; }
        public int NumberOfPages { get; set; }
    }
}
