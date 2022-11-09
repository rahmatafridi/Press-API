using PressDot.Contracts;
using PressDot.Contracts.Request.Saloon;
using PressDot.Contracts.Request.SaloonLaboratory;
using PressDot.Contracts.Response.Saloon;
using PressDot.Contracts.Response.SaloonLaboratory;
using PressDot.Core.Domain;
using PressDot.Core.Enums;
using PressDot.Core.Exceptions;
using PressDot.Facade.Framework.Extensions;
using PressDot.Facade.Infrastructure;
using PressDot.Service.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace PressDot.Facade.Implementation
{
    public class SaloonFacade : ISaloonFacade
    {
        #region private

        private readonly ISaloonService _saloonService;

        private readonly ILocationService _locationService;

        private readonly ISaloonTypeService _saloonTypeService;

        private readonly IAppointmentService _appointmentService;

        private readonly IStatesService _statesService;

        private readonly ISaloonLaboratoryService _saloonLaboratoryService;

        private readonly ILaboratoryService _laboratoryService;

        private readonly IOrderService _orderService;

        private readonly ISaloonEmployeeService _saloonEmployeeService;

        private readonly IDaysOfWeekService _daysOfWeekService;

        private readonly ISaloonEmployeeScheduleService _saloonEmployeeScheduleService;

        private readonly IUsersService _usersService;
        #endregion


        #region ctor

        public SaloonFacade(ISaloonService saloonService, ILocationService locationService, ISaloonTypeService saloonTypeService
            , IAppointmentService appointmentService, IStatesService statesService, ISaloonLaboratoryService saloonLaboratoryService,
            ILaboratoryService laboratoryService, IOrderService orderService,
            ISaloonEmployeeService saloonEmployeeService, IDaysOfWeekService daysOfWeekService,
            ISaloonEmployeeScheduleService saloonEmployeeScheduleService, IUsersService usersService)
        {
            _saloonService = saloonService;
            _locationService = locationService;
            _saloonTypeService = saloonTypeService;
            _appointmentService = appointmentService;
            _statesService = statesService;
            _saloonLaboratoryService = saloonLaboratoryService;
            _laboratoryService = laboratoryService;
            _orderService = orderService;
            _saloonEmployeeService = saloonEmployeeService;
            _daysOfWeekService = daysOfWeekService;
            _saloonEmployeeScheduleService = saloonEmployeeScheduleService;
            _usersService = usersService;
        }
        #endregion

        #region methods

        public PressDotPageListEntityModel<GetSaloonsResponse> GetSaloons(string name, int? countryId, int? cityId, int pageIndex, int pageSize)
        {
            var saloons = _saloonService.GetSaloons(name, countryId, cityId, pageIndex, pageSize);
            if (saloons == null)
                throw new PressDotNotFoundException(1021, $"No Saloon exist in system.");
            var locations = _locationService.Get(saloons.Select(x => x.CountryId).ToArray().Concat(saloons.Select(y => y.CityId).ToArray()).ToArray());
            foreach (var saloon in saloons)
            {
                saloon.Country = locations.FirstOrDefault(x => x.Id == saloon.CountryId);
                saloon.City = locations.FirstOrDefault(x => x.Id == saloon.CityId);
            }

            var saloonTypes = _saloonTypeService.Get();
            foreach (var saloon in saloons)
            {
                saloon.SaloonType = saloonTypes.FirstOrDefault(x => x.Id == saloon.SaloonTypeId);
            }
            var saloonPagedList = new PressDotPageListEntityModel<GetSaloonsResponse>()
            {
                Data = saloons.ToModel<GetSaloonsResponse>(),
                HasNextPage = saloons.HasNextPage,
                HasPreviousPage = saloons.HasPreviousPage,
                PageIndex = saloons.PageIndex,
                PageSize = saloons.PageSize,
                TotalCount = saloons.TotalCount,
                TotalPages = saloons.TotalPages
            };

            return saloonPagedList;
        }

        public ICollection<GetSaloonsResponse> GetSaloonsByLocation(string location)
        {
            var locationId = _locationService.GetLocationIdByName(location);
            if (locationId != null && locationId > 0)
            {
                var saloons = _saloonService.GetSaloonsByLocationId(locationId ?? 0);
                var data = saloons.ToModel<GetSaloonsResponse>();
                if (data.Count > 0)
                {
                    return data;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                throw new PressDotNotFoundException(1021, $"Invalid location name.");
            }


        }

        public ICollection<GetSaloonsResponse> GetSaloonsByLocationOrSaloonName(string location, string saloonName)
        {
            if (!string.IsNullOrEmpty(location))
            {
                var locationId = _locationService.GetLocationIdByName(location);
                if (locationId != null && locationId > 0)
                {
                    var saloons = _saloonService.GetSaloonsByLocationId(locationId ?? 0);
                    if (saloons == null)
                        return null;
                    if (!string.IsNullOrEmpty(saloonName)) // Filter if saloon and location are provided
                        saloons = saloons.Where(x => x.SaloonName.Contains(saloonName)).ToList();
                    var saloonLabs = _saloonLaboratoryService.GetSaloonLaboratoriesBySaloonIds(saloons.Select(x => x.Id).ToArray());

                    saloons = saloons.Where(x => saloonLabs.Select(y => y.SaloonId).ToArray().Contains(x.Id)).ToList(); // Only get those saloons which have Labs.

                    if (saloons.Count == 0)
                        return null;
                    return saloons.ToModel<GetSaloonsResponse>();
                    
                }
            }
            else if (!string.IsNullOrEmpty(saloonName))
            {
                var saloons = _saloonService.GetSaloons(saloonName);
                if (saloons.Count == 0)
                    return null;
                var saloonLabs = _saloonLaboratoryService.GetSaloonLaboratoriesBySaloonIds(saloons.Select(x => x.Id).ToArray());
                saloons = saloons.Where(x => saloonLabs.Select(y => y.SaloonId).ToArray().Contains(x.Id)).ToList(); // Only get those saloons which have Labs.

                if (saloons.Count == 0)
                    return null;
                var saloonIds =
                    CheckSaloonAvailability(saloons.Select(x => x.Id).ToArray(), DateTime.Today, DateTime.Now.AddDays(28)); // Get all those saloons who are available for next 28 days.
                return saloons.Where(x=>saloonIds.Contains(x.Id)).ToList().ToModel<GetSaloonsResponse>();
            }
            else
            {
                throw new PressDotNotFoundException(1021, $"Invalid location name or saloon name.");
            }

            return null;
        }

        public ICollection<GetSaloonsResponse> GetSaloonsByLocationId(int locationId)
        {
            var saloons = _saloonService.GetSaloonsByLocationId(locationId);
            var data = saloons.ToModel<GetSaloonsResponse>();
            if (data.Count > 0)
            {
                return data;
            }
            else
            {
                return null;
            }
        }

        public ICollection<GetSaloonAppointmentsDto> GetSaloonAppointments(int saloonId, DateTime? date)
        {
            var dayId = 0;

            var startDate = new DateTime();
            var endDate = new DateTime();

            if (date == null)
            {
                startDate = DateTime.Today;
                endDate = DateTime.Today.AddDays(28);
            }
            else
            {
                startDate = date.Value;
                endDate = date.Value.AddDays(1).Date;
            }

            

            var saloonEmployeeIds = _saloonEmployeeService.GetSaloonEmployeeIdsBySaloonId(saloonId);

            var saloonEmployees = _saloonEmployeeService.GetSaloonEmployeesByIds(saloonEmployeeIds);

            var listToReturn = new List<GetSaloonAppointmentsDto>();

            var saloonAppointments = _appointmentService.GetSaloonsAppointmentsAfterDate(new []{saloonId}, DateTime.Today);

            foreach (var se in saloonEmployees)
            {
                

                var user = _usersService.Get(se.EmployeeId);
                var saloon = _saloonService.Get(se.SaloonId);


                while (startDate <= endDate)
                {

                    var saloonEmployeeSchedules =
                        _saloonEmployeeScheduleService.GetSaloonEmployeeSchedules_BySaloonEmployeeIdAndDayId(se.Id,
                            (int)startDate.DayOfWeek);
                    if (saloonEmployeeSchedules.Any())
                        foreach (var ses in saloonEmployeeSchedules)
                        {
                            var slot = new GetSaloonAppointmentsDto()
                            {

                                SaloonEmployeeScheduleId = ses.Id,
                                DoctorId = user.Id,
                                DoctorName = user.Firstname + " " + user.Lastname,
                                SaloonId = saloon.Id,
                                SaloonName = saloon.SaloonName,
                                Date = startDate,
                                StartTime = ses.StartTime.ToString(),
                                EndTime = ses.EndTime.ToString(),
                                Email = saloon.Email,
                                Phone = saloon.Phone,
                                Address = saloon.Address,
                                Slots = GetSlots(ses.StartTime, ses.EndTime, saloonId, se.EmployeeId, startDate,
                                    saloonAppointments)
                            };

                            listToReturn.Add(slot);
                        }

                    startDate = startDate.AddDays(1);
                }
            }

            return listToReturn.OrderBy(x=>x.StartTime).ToList();

        }

        

        public Saloon GetSaloonById(int saloonId)
        {
            return _saloonService.GetSaloonById(saloonId);
        }

        
        public GetSaloonsResponse UpdateSaloon(SaloonUpdateRequest UpdateSaloon)
        {
            var saloon = UpdateSaloon.ToEntity<Saloon>();

            return _saloonService.UpdateSaloon(saloon).ToModel<GetSaloonsResponse>();
        }



        public bool DeleteSaloon(int saloonId)
        {
            var stateIds = _statesService.GetStates().Where(s => s.StateFor == "Appointment" && s.Value == "Pending")
                .Select(s => s.Id).ToArray();

            var saloon = _saloonService.GetSaloonById(saloonId);

            if (saloon != null)
            {
                //Don't let saloon delete if system finds any appointments in pending state against the saloon.
                var isDeletable = _appointmentService.GetAppointmentsBySaloonAndSates(saloon.Id, stateIds).Any() == false;

                if (isDeletable)
                {
                    return _saloonService.DeleteSaloon(saloon);
                }
                else
                {
                    throw new PressDotException((int)SaloonExceptionsCodes.Pending_Appointments_Found_For_Saloon, "Unable to delete, pending appointments found for saloon.");
                }
            }
            else
            {
                throw new PressDotException((int)SaloonExceptionsCodes.Saloon_Not_Found, "Saloon not found.");
            }
        }

        public bool DeleteSaloonLaboratory(int saloonLaboratoryId)
        {
            var saloonLaboratory = _saloonLaboratoryService.Get(saloonLaboratoryId);

            if (saloonLaboratory != null)
            {
                var isAnyPedningOrders = _orderService.IsAnyPendingOrdersByLaboratory(saloonLaboratory.LaboratoryId);

                if (isAnyPedningOrders == false)
                {
                    return _saloonLaboratoryService.DeleteSaloonLaboratory(saloonLaboratoryId);
                }
                else
                {
                    throw new PressDotException((int)SaloonLaboratoryExceptionsCodes.Unable_To_Delete_Laboratory_Due_To_Penidng_Orders,
                                                "Unable to delete saloon laboratory, pending orders found.");
                }

            }
            else
            {
                throw new PressDotException((int)GeneralExceptionsCodes.Invalid_Id, "Invalid SaloonLaboratoryId.");
            }

        }

        public SaloonLaboratoryResponse ChangeDefaultLaboratory(SaloonLaboratoryCreateRequest model)
        {
            var dbObj = _saloonLaboratoryService.Get()
                .FirstOrDefault(x =>
                    x.SaloonId == model.SaloonId && x.LaboratoryId == model.LaboratoryId && x.IsDeleted == false);

            //Make sure saloon exists
            var Saloon = _saloonService.GetSaloonById(model.SaloonId);
            if (Saloon == null)
            {
                throw new PressDotException((int)SaloonLaboratoryExceptionsCodes.Saloon_Not_Found, "Saloon not found.");
            }

            //make sure laboratory exists
            var lab = _laboratoryService.Get().FirstOrDefault(x => x.Id == model.LaboratoryId);
            if (lab == null)
            {
                throw new PressDotException((int)SaloonLaboratoryExceptionsCodes.Laboratory_Not_Found, "Laboratory not found.");
            }

            //if saloon and labs exists and already associated
            if (dbObj != null)
            {
                //check if already defualt lab for saloon
                if (dbObj.LaboratoryId == model.LaboratoryId && dbObj.IsDefault == model.IsDefault)
                {
                    throw new PressDotException((int)SaloonLaboratoryExceptionsCodes.Laboratory_Already_Attached,
                        "This laboratory already attached with saloon.");
                }


                //make lab as default for saloon.
                var entity = model.ToEntity<SaloonLaboratory>();
                entity.Id = dbObj.Id;
                var result =
                    _saloonLaboratoryService.ChangeSaloonDefaultLaboratory(entity, isUpdate: true);
                return result.ToModel<SaloonLaboratoryResponse>();
            }
            else
            {
                //attach lab with saloon and make it default.
                var result =
                    _saloonLaboratoryService.ChangeSaloonDefaultLaboratory(model.ToEntity<SaloonLaboratory>(),
                        isUpdate: false);

                return result.ToModel<SaloonLaboratoryResponse>();
            }

        }

        public List<SaloonLaboratoryResponse> GetLaboratoriesBySaloonId(int saloonId)
        {
            var isValidSaloon = _saloonService.CheckSaloonById(saloonId);
            var listToReturn = new List<SaloonLaboratoryResponse>();
            if (isValidSaloon)
            {
                var labs = _saloonLaboratoryService.GetSaloonLaboratoriesBySaloonId(saloonId);


                if (labs.Any())
                {
                    foreach (var lab in labs)
                    {
                        var vmLab = lab.ToModel<SaloonLaboratoryResponse>();
                        vmLab.LaboratoryName = lab.Laboratory.LaboratoryName;
                        listToReturn.Add(vmLab);
                    }
                }
            }
            else
            {
                throw new PressDotException((int)SaloonLaboratoryExceptionsCodes.Saloon_Not_Found, "Invalid SaloonId.");
            }

            return listToReturn;
        }

        public ICollection<Saloon> GetSaloon(string searchTerm)
        {
            return _saloonService.GetSaloons(searchTerm);
        }

        #endregion

        #region private methods

        private int[] CheckSaloonAvailability(int[] saloonIds, DateTime startDate, DateTime endDate)
        {
            var saloonEmployees = _saloonEmployeeService.GetSaloonEmployees(saloonIds);
            var saloonEmployeesSchedules =
                _saloonEmployeeScheduleService.GetSaloonEmployeeSchedulesBySaloonEmployeeIds(saloonEmployees.Select(x => x.Id)
                    .ToArray());
            var saloonAppointments = _appointmentService.GetSaloonsAppointmentsAfterDate(saloonIds, DateTime.Today);
            var availableSaloonList = new List<int>();

            foreach (var saloonEmployeeSchedule in saloonEmployeesSchedules)
            {

                var saloonEmployee =
                    saloonEmployees.FirstOrDefault(x => x.Id == saloonEmployeeSchedule.SaloonEmployeeId);
                if (availableSaloonList.Contains(saloonEmployee.SaloonId)) continue;

                var saloonAppointment = saloonAppointments.Where(x =>
                    x.SaloonId == saloonEmployee.SaloonId && x.DoctorId == saloonEmployee.EmployeeId).ToList();
                var tempStartDate = startDate;
                while (tempStartDate <= endDate)
                {
                    if ((int)tempStartDate.DayOfWeek == saloonEmployeeSchedule.Day - 1)
                    {


                        var slotDtos = GetSlots(saloonEmployeeSchedule.StartTime, saloonEmployeeSchedule.EndTime,
                            saloonEmployee.SaloonId, saloonEmployee.EmployeeId, tempStartDate.Date,
                            saloonAppointment);
                        if (slotDtos.Count > 0)
                            availableSaloonList.Add(saloonEmployee.SaloonId);
                    }

                    tempStartDate = tempStartDate.AddDays(1);
                }

            }

            return availableSaloonList.ToArray();
        }

        private List<SlotDto> GetSlots(TimeSpan startTime, TimeSpan endTime, int saloonId, int docId, DateTime date)
        {


            var duration = endTime - startTime;

            var minutes = duration.TotalMinutes;
            var slotList = new List<SlotDto>();
            if (minutes > 20)
            {
                var totalSlots = (int)minutes / 20;
                var startingMinutes = 00;
                var endMinutes = 20;
                int startingHour = startTime.Hours, endingHour = startTime.Hours;
                var firstSlotStartTime = startTime.Hours + ":00";
                var firstSlotEndTime = startTime.Hours + ":" + endMinutes;

                for (var i = 1; i <= totalSlots; i++)
                {
                    if (i > 1)
                    {
                        startingMinutes += 20;
                        endMinutes += 20;
                    }

                    if (endMinutes == 60)
                    {

                        endMinutes = 0;
                        endingHour++;
                    }

                    if (startingMinutes == 60)
                    {

                        startingHour += 1;
                        startingMinutes = 0;
                        endMinutes = 20;
                    }


                    var computedStartTime = i == 1
                        ? firstSlotStartTime
                        : startingHour + ":" + (startingMinutes == 0 ? "00" : startingMinutes.ToString());
                    var computedEndTime = i == 1
                        ? firstSlotEndTime
                        : endingHour + ":" + (endMinutes == 0 ? "00" : endMinutes.ToString());
                    var slot = new SlotDto()
                    {

                        StartHour = computedStartTime,
                        EndHour = computedEndTime,
                        IsOccupied = _appointmentService.IsSlotOccupied(saloonId, docId, date, computedStartTime, computedEndTime)
                    };

                    var slotDateTime = date;
                    slotDateTime = slotDateTime.Add(new TimeSpan(startingHour, startingMinutes, 0));
                    if (slotDateTime.Subtract(DateTime.Now).TotalMinutes > 10)
                        slotList.Add(slot);


                }
            }

            return slotList;
        }

        private List<SlotDto> GetSlots(TimeSpan startTime, TimeSpan endTime, int saloonId, int docId,  DateTime date,ICollection<Appointment> appointments)
        {

            var pendingStatusId = _statesService.GetStatesByStateNameAndType("Pending", "Appointment").Id;
            var approvedStatusId = _statesService.GetStatesByStateNameAndType("Approved", "Appointment").Id;
            var duration = endTime - startTime;

            var minutes = duration.TotalMinutes;
            var slotList = new List<SlotDto>();
            if (minutes > 20)
            {
                var totalSlots = (int)minutes / 20;
                var startingMinutes = 00;
                var endMinutes = 20;
                int startingHour = startTime.Hours, endingHour = startTime.Hours;
                var firstSlotStartTime = startTime.Hours + ":00";
                var firstSlotEndTime = startTime.Hours + ":" + endMinutes;

                for (var i = 1; i <= totalSlots; i++)
                {
                    if (i > 1)
                    {
                        startingMinutes += 20;
                        endMinutes += 20;
                    }

                    if (endMinutes == 60)
                    {

                        endMinutes = 0;
                        endingHour++;
                    }

                    if (startingMinutes == 60)
                    {

                        startingHour += 1;
                        startingMinutes = 0;
                        endMinutes = 20;
                    }


                    var computedStartTime = i == 1
                        ? firstSlotStartTime
                        : startingHour + ":" + (startingMinutes == 0 ? "00" : startingMinutes.ToString());
                    var computedEndTime = i == 1
                        ? firstSlotEndTime
                        : endingHour + ":" + (endMinutes == 0 ? "00" : endMinutes.ToString());
                    var sTime = TimeSpan.Parse(computedStartTime);
                    var eTime = TimeSpan.Parse(computedEndTime);
                    var slot = new SlotDto()
                    {

                        StartHour = computedStartTime,
                        EndHour = computedEndTime,
                        IsOccupied = appointments.Any(x=>x.StartTime == sTime && x.EndTime == eTime
                                                                              && x.SaloonId == saloonId &&
                                                                              x.DoctorId == docId
                                                                              && (x.StateId == pendingStatusId || x.StateId == approvedStatusId)
                                                                              && x.Date.Date == date.Date)
                    };

                    var slotDateTime = date;
                    slotDateTime = slotDateTime.Add(new TimeSpan(startingHour, startingMinutes, 0));
                    if (slotDateTime.Subtract(DateTime.Now).TotalMinutes > 10)
                        slotList.Add(slot);


                }
            }

            return slotList;
        }
        #endregion
    }
}
