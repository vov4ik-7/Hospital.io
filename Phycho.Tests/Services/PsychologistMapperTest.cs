using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Psycho.DAL.Core.Domain;
using Psycho.DTO.Core;
using Psycho.DTO.Persistence;
using Psycho.Logic.DataMappers;
using Xunit;

namespace Phycho.Tests.Services
{
    public class PsychologistMapperTest
    {
        [Fact]
        public void TestPsychologistMapper()
        {
            var mock = new Mock<PsychologistMapper>();
            mock.Setup(mapper => mapper.Map(GetTestPsychologists())).Returns(GetTestPsychologistListDto());
            
            var resultMapper = new PsychologistMapper();
            var result = resultMapper.Map(GetTestPsychologists());

            Assert.Equal(result.ColumnHeaders, GetTestPsychologistListDto().ColumnHeaders);
            Assert.Equal(result.PsychologistDTOs.Count, GetTestPsychologistListDto().PsychologistDTOs.Count);
            Assert.Equal(result.GeneralColumnHeaders, GetTestPsychologistListDto().GeneralColumnHeaders);
        }

        public List<Psychologist> GetTestPsychologists()
        {
            int n = 10;
            var psychologists = new List<Psychologist>(n);
            for (var i = 1; i <= n; ++i)
            {
                psychologists.Add(new Psychologist
                {
                    Id = i,
                    Email = "Email " + i,
                    UserName = "UserName " + i,
                    FirstName = "FirstName " + i,
                    LastName = "LastName " + i,
                    Address = "Address " + i,
                    Gender = Gender.Female,
                    PhoneNumber = "PhoneNumber " + i,
                    Position = "Position " + i,
                    HireDate = DateTime.Now
                });
            }

            return psychologists;
        }

        public PsychologistListDTO GetTestPsychologistListDto()
        {
            var psychologistsDto = new PsychologistListDTO();

            for (var i = 1; i <= 10; ++i)
            {
                psychologistsDto.PsychologistDTOs.Add(new PsychologistDTO
                {
                    Id = i,
                    Email = "Email " + i,
                    UserName = "UserName " + i,
                    FirstName = "FirstName " + i,
                    LastName = "LastName " + i,
                    Address = "Address " + i,
                    Gender = Gender.Female,
                    Phone = "PhoneNumber " + i,
                    Position = "Position " + i,
                    HireDate = DateTime.Now
                });
            }

            return psychologistsDto;
        }
    }
}
