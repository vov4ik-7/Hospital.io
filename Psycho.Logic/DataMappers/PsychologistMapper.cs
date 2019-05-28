using System;
using System.Collections.Generic;
using System.Linq;
using Psycho.DAL.Core.Domain;
using Psycho.DTO.Persistence;

namespace Psycho.Logic.DataMappers
{
    public class PsychologistMapper
    {
        public PsychologistMapper()
        {
        }

        public PsychologistListDTO Map(List<Psychologist> psychologists, string dayOfWeek)
        {
            PsychologistListDTO psychologistListDTO = new PsychologistListDTO();

            foreach(var elem in psychologists)
            {
                Day day = (Day)Enum.Parse(typeof(Day), dayOfWeek);
                var kek = elem.WorkSchedules.Where(s => s.Day == day).FirstOrDefault();
                string start = kek != null ? kek.StartTime.ToString() : "";
                string end = kek != null ? kek.EndTime.ToString() : "";

                psychologistListDTO.PsychologistDTOs.Add(new DTO.Core.PsychologistDTO {
                    Id = elem.Id,
                    Email = elem.Email,
                    UserName = elem.UserName,
                    FirstName = elem.FirstName,
                    LastName = elem.LastName,
                    Address = elem.Address,
                    Gender = elem.Gender,
                    Phone = elem.PhoneNumber,
                    Position = elem.Position,
                    HireDate = elem.HireDate,
                    StartTimeForToday = start,
                    EndTimeForToday = end
                });
            }

            return psychologistListDTO;
        }
    }
}
