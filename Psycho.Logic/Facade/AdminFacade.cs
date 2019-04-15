using System;
using System.Collections.Generic;
using System.Linq;
using Psycho.DAL.Core;
using Psycho.DAL.Core.Domain;
using Psycho.DTO.Persistence;
using Psycho.Logic.DataMappers;
using Psycho.Logic.Facade.Interfaces;

namespace Psycho.Logic.Facade
{
    public class AdminFacade : IAdminFacade
    {
        private IUnitOfWork _unitOfWork;
        private PsychologistMapper psychologistMapper;

        public AdminFacade(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
            psychologistMapper = new PsychologistMapper();
        }

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return this._unitOfWork;
            }
        }

        public PsychologistListDTO GetPsychologistListForAdminPage()
        {
            List<Psychologist> psychologists = _unitOfWork.Psychologists.GetAll().ToList();
            PsychologistListDTO psychologistListDTO = psychologistMapper.Map(psychologists);

            return psychologistListDTO;
        }
    }
}
