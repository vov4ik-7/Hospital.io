using System;
using System.Collections.Generic;
using Psycho.DAL.Core.Domain;
using Psycho.DTO.Persistence;

namespace Psycho.Logic.DataMappers
{
    public class PsychologistMapper
    {
        public PsychologistMapper()
        {
        }

        public virtual PsychologistListDTO Map(List<Psychologist> psychologists)
        {
            PsychologistListDTO psychologistListDTO = new PsychologistListDTO();

            foreach(var elem in psychologists)
            {
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
                    HireDate = elem.HireDate
                });
            }

            return psychologistListDTO;
        }
    }
}
