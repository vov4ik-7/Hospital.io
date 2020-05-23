using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Psycho.DAL.Core;
using Psycho.DAL.Core.Domain;
using Psycho.DTO.Core;
using Psycho.DTO.Persistence;
using Psycho.Logic.Facade.Interfaces;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Psycho.io.Controllers
{
    [AllowAnonymous]
    public class PsychologistController : PsychoMvcControllerBase
    {
        private readonly UserManager<User> _userManager;
        private IUnitOfWork _unitOfWork;

        public PsychologistController(IUnitOfWork unitOfWork, IPsychoLogic psychoLogic,
            UserManager<User> userManager)
            : base(psychoLogic)
        {
            this._unitOfWork = unitOfWork;
            this._userManager = userManager;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            PsychologistListDTO psychologistListDTO = PsychoLogic.AdminFacade.GetPsychologistListForAdminPage();

            return View(psychologistListDTO);
        }

        [HttpGet]
        public IActionResult Schedule(int id)
        {
            Psychologist psychologist = _unitOfWork.Psychologists.Get(id);
            var appointments = psychologist.Appointments.ToList();

            var model = new PsychologistWithAppointmentsDTO();
            model.Name = $"{psychologist.FirstName} {psychologist.LastName}";
            model.Psychologist = psychologist;

            List<AppointmentForCalendarDTO> list = new List<AppointmentForCalendarDTO>();
            foreach (var elem in appointments)
            {
                string sdtm = elem.StartDataTime?.Month <= 9 ? $"0{elem.StartDataTime?.Month}" : $"{elem.StartDataTime?.Month}";
                string sdtd = elem.StartDataTime?.Day <= 9 ? $"0{elem.StartDataTime?.Day}" : $"{elem.StartDataTime?.Day}";
                string sdth = elem.StartDataTime?.Hour <= 9 ? $"0{elem.StartDataTime?.Hour}" : $"{elem.StartDataTime?.Hour}";
                string sdtmin = elem.StartDataTime?.Minute <= 9 ? $"0{elem.StartDataTime?.Minute}" : $"{elem.StartDataTime?.Minute}";


                string edtm = elem.EndDataTime?.Month <= 9 ? $"0{elem.EndDataTime?.Month}" : $"{elem.EndDataTime?.Month}";
                string edtd = elem.EndDataTime?.Day <= 9 ? $"0{elem.EndDataTime?.Day}" : $"{elem.EndDataTime?.Day}";
                string edth = elem.EndDataTime?.Hour <= 9 ? $"0{elem.EndDataTime?.Hour}" : $"{elem.EndDataTime?.Hour}";
                string edtmin = elem.EndDataTime?.Minute <= 9 ? $"0{elem.EndDataTime?.Minute}" : $"{elem.EndDataTime?.Minute}";

                model.appointments.Add(new AppointmentForCalendarDTO
                {
                    id = elem.Id.ToString(),
                    title = "Busy",
                    description = "",
                    //title = $"{elem.AuthorizedUser.FirstName} {elem.AuthorizedUser.LastName}",
                    //description = elem.AdditionalInfo,
                    start = $"{elem.StartDataTime?.Year}-{sdtm}-{sdtd}T{sdth}:{sdtmin}:00",
                    end = $"{elem.EndDataTime?.Year}-{edtm}-{edtd}T{edth}:{edtmin}:00"
                });
            }

            model.json = JsonConvert.SerializeObject(model.appointments);
            return PartialView(model);
        }

        [HttpGet]
        public async Task<IActionResult> CreateAppointment(int id)
        {
            var userId = await GetCurrentUserId();
            CreateAppointmentDTO createAppointmentDTO = new CreateAppointmentDTO();

            createAppointmentDTO.CurrentPsychologist = _unitOfWork.Psychologists.Get(id);

            createAppointmentDTO.CurrentAuthorizedUser = _unitOfWork.AuthorizedUsers.Get(userId);

            createAppointmentDTO.Psychologists = new List<Psychologist>();//_unitOfWork.Psychologists.GetAll().ToList();
            createAppointmentDTO.AuthorizedUsers = new List<AuthorizedUser>();//_unitOfWork.AuthorizedUsers.GetAll().ToList();

            return PartialView(createAppointmentDTO);
        }

        [HttpPost]
        public IActionResult CreateAppointment(CreateAppointmentDTO model)
        {
            string _status = String.Empty;
            string _description = String.Empty;

            List<Appointment> appointments = _unitOfWork.Psychologists.Get(model.PsychologistId).Appointments.ToList();

            string currDayOfWeek = DateTime.Now.DayOfWeek.ToString();
            Day day = (Day)Enum.Parse(typeof(Day), currDayOfWeek);
            //var kek = model.CurrentPsychologist.WorkSchedules.Where(s => s.Day == day).FirstOrDefault();
            var kek = _unitOfWork.Psychologists.Get(model.PsychologistId).WorkSchedules.Where(s => s.Day == day).FirstOrDefault();


            foreach (var elem in appointments)
            {
                if ((model.StartDateTime >= elem.StartDataTime && model.StartDateTime <= elem.EndDataTime) ||
                    (model.EndDateTime >= elem.StartDataTime && model.EndDateTime <= elem.EndDataTime) ||
                    (elem.StartDataTime >= model.StartDateTime && elem.EndDataTime <= model.EndDateTime))
                {
                    _status = "error";
                    _description = "Doctor is busy at that time.";

                    break;
                }
            }

            if (_status != "error")
            {
                if (!(model.StartDateTime?.Hour >= kek?.StartTime.Hours &&
                        model.StartDateTime?.Minute >= kek?.StartTime.Minutes &&
                        model.EndDateTime?.Hour < kek?.EndTime.Hours))
                {
                    _status = "error";
                    _description = "Doctor does not work at this time.";
                }
            }

            if (_status != "error")
            {
                Appointment appointment = new Appointment
                {
                    PsychologistId = model.PsychologistId,
                    AuthorizedUserId = model.AuthorizedUserId,
                    StartDataTime = model.StartDateTime,
                    EndDataTime = model.EndDateTime,
                    AdditionalInfo = model.Info
                };

                _unitOfWork.Appointments.Add(appointment);
                int result = _unitOfWork.Complete();

                _status = "success";
                _description = "Appointment created.";
            }
            //if (User.IsInRole("Doctor"))
            {
                //return RedirectToAction("DashboardDoctor", "User", new { showAll = false });
            }
            //else
            {
                //return RedirectToAction("DashboardPatient", "User", new { showAll = false });
            }

            return Json(new { status = _status, description = _description });
        }

        [HttpGet]
        public IActionResult Reports(int id)
        {
            var psychologist = _unitOfWork.Psychologists.Get(id);
            ReportsDTO reportsDTO = new ReportsDTO();
            reportsDTO.Psychologist = psychologist;

            var reportsFromDB = _unitOfWork.Reports.GetAll().Where(r => r.PsychologistId == id && r.IsVisible == true).OrderByDescending(r => r.Id).ToList();
            List<ReportDTO> reportDTOs = new List<ReportDTO>();
            /*for (int i = 0; i < reportsFromDB.Count; i++)
            {
                if(reportsFromDB[i].IsAnonymous)
                {
                    reportsFromDB[i].AuthorizedUser.FirstName = "Suka" + i;
                    reportsFromDB[i].AuthorizedUser.LastName = "";
                }
            }
            reportsDTO.Reports = reportsFromDB;*/
            foreach(var elem in reportsFromDB)
            {
                reportDTOs.Add(new ReportDTO
                {
                    Message = elem.Message,
                    IsAnonymous = elem.IsAnonymous,
                    FirstName = elem.IsAnonymous ? "Anonymous" : elem.AuthorizedUser.FirstName,
                    LastName = elem.IsAnonymous ? "" : elem.AuthorizedUser.LastName
                });
            }

            reportsDTO.Reports = reportDTOs;

            return PartialView(reportsDTO);
        }

        [HttpGet]
        public IActionResult AddReport(int id)
        {
            CreateReportDTO createReportDTO = new CreateReportDTO();

            var psychologist = _unitOfWork.Psychologists.Get(id);
            var authorizedUser = GetCurrentUserAsync().Result;

            createReportDTO.PsychologistId = id;
            createReportDTO.PsychologistName = $"{psychologist.FirstName} {psychologist.LastName}";
            createReportDTO.AuthorizedUserId = authorizedUser.Id;
            createReportDTO.AuthorizedUserName = $"{authorizedUser.FirstName} {authorizedUser.LastName}";
            createReportDTO.IsChecked = false;

            return PartialView(createReportDTO);
        }

        [HttpPost]
        public IActionResult AddReport(CreateReportDTO model)
        {
            string _status = String.Empty;
            string _description = String.Empty;

            Report report = new Report()
            {
                Message = model.Message,
                PsychologistId = model.PsychologistId,
                AuthorizedUserId = model.AuthorizedUserId,
                IsAnonymous = model.IsChecked
            };

            _unitOfWork.Reports.Add(report);
            _unitOfWork.Complete();

            _status = "success";
            _description = "Report added.";

            //if (_status != "error")
            //{
            //    Appointment appointment = new Appointment
            //    {
            //        PsychologistId = model.PsychologistId,
            //        AuthorizedUserId = model.AuthorizedUserId,
            //        StartDataTime = model.StartDateTime,
            //        EndDataTime = model.EndDateTime,
            //        AdditionalInfo = model.Info
            //    };

            //    _unitOfWork.Appointments.Add(appointment);
            //    int result = _unitOfWork.Complete();

            //    _status = "success";
            //    _description = "Appointment created.";
            //}
            //if (User.IsInRole("Doctor"))
            {
                //return RedirectToAction("DashboardDoctor", "User", new { showAll = false });
            }
            //else
            {
                //return RedirectToAction("DashboardPatient", "User", new { showAll = false });
            }

            return Json(new { status = _status, description = _description });
        }

        [HttpGet]
        public async Task<int> GetCurrentUserId()
        {
            User usr = await GetCurrentUserAsync();
            return usr.Id;
        }

        private Task<User> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
    }
}
