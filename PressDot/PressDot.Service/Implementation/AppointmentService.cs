using PressDot.Core;
using PressDot.Core.Data;
using PressDot.Core.Domain;
using PressDot.Service.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PressDot.Service.Implementation
{
    public class AppointmentService : BaseService<Appointment>, IAppointmentService
    {
        #region private members

        private readonly IStatesService _stateService;
        private readonly ILaboratoryUsersService _laboratoryUsersService;
        private readonly ISaloonLaboratoryService _saloonLaboratoryService;
        private readonly ISaloonService _saloonService;
        #endregion

        #region ctor

        public AppointmentService(ISaloonService saloonService, ISaloonLaboratoryService saloonLaboratoryService,ILaboratoryUsersService laboratoryUsersService ,IRepository<Appointment> repository, IStatesService stateService) : base(repository)
        {
            _stateService = stateService;
            _laboratoryUsersService = laboratoryUsersService;
            _saloonLaboratoryService = saloonLaboratoryService;
            _saloonService = saloonService;
        }

        #endregion

        #region methods
        public IPagedList<Appointment> GetAppointmentsbySaloonId(int saloonId, int pageIndex, int pageSize)
        {
            var query = Repository.Table;
            query = query.Where(ap => ap.SaloonId == saloonId);
            return new PagedList<Appointment>(query, pageIndex, pageSize);
        }

        public IPagedList<Appointment> GetAppointmentsbySaloonIdAndState(int saloonId,int stateId, int pageIndex,
            int pageSize)
        {
            var query = Repository.Table.Where(x => x.SaloonId == saloonId && x.StateId == stateId && x.Date >= DateTime.UtcNow.Date);
            return new PagedList<Appointment>(query, pageIndex, pageSize);
        }

        public List<Appointment> GetAppointmentsBySaloonId(int saloonId)
        {
            var query = Repository.Table;
            query = query.Where(ap => ap.SaloonId == saloonId);
            return query.ToList();
        }
        public IPagedList<Appointment> GetAppointmentsbySaloonId(int saloonId, DateTime date, int pageIndex, int pageSize)
        {
            var query = Repository.Table;
            query = query.Where(ap => ap.SaloonId == saloonId && ap.Date.Date.Equals(date.Date));
            return new PagedList<Appointment>(query, pageIndex, pageSize);
        }

        public IPagedList<Appointment> GetAppointmentsbyDoctorId(int doctorId, int pageIndex, int pageSize)
        {
            var query = Repository.Table;
            query = query.Where(ap => ap.DoctorId == doctorId && !ap.IsDeleted);
            return new PagedList<Appointment>(query, pageIndex, pageSize);
        }
        public List<Appointment> GetAppointmentsbyDoctorId(int doctorId)
        {
            var query = Repository.Table;
            query = query.Where(ap => ap.DoctorId == doctorId && !ap.IsDeleted);
            return query.ToList();
        }
        public IPagedList<Appointment> GetAppointmentsbyCustomerId(int customerId, int pageIndex, int pageSize)
        {
            var query = Repository.Table;
            query = query.Where(ap => ap.CustomerId == customerId);
            return new PagedList<Appointment>(query, pageIndex, pageSize);
        }
        public IPagedList<Appointment> GetAppointmentsbyAppointmentState(List<int> stateIds, int pageIndex, int pageSize)
        {
            var query = Repository.Table;
            query = query.Where(ap => stateIds.Contains(ap.StateId));
            return new PagedList<Appointment>(query, pageIndex, pageSize);
        }

        public IPagedList<Appointment> GetSaloonAppointmentsForSaloonAdministrator(List<int> stateIds,int saloonId, int pageIndex, int pageSize)
        {
            var query = Repository.Table;
            query = query.Where(ap => stateIds.Contains(ap.StateId) && ap.SaloonId == saloonId);
            return new PagedList<Appointment>(query, pageIndex, pageSize);
        }

        public bool UpdateAppointment(Appointment appointment)
        {
            var dbAppointmentObj = Get(appointment.Id);
            if (dbAppointmentObj != null)
            {
                dbAppointmentObj.CustomerId = appointment.CustomerId;
                dbAppointmentObj.DoctorId = appointment.DoctorId;
                dbAppointmentObj.SaloonId = appointment.SaloonId;
                dbAppointmentObj.StateId = appointment.StateId;
                dbAppointmentObj.Symptoms = appointment.Symptoms;
                dbAppointmentObj.UpdatedBy = appointment.UpdatedBy;
                dbAppointmentObj.UpdatedDate = DateTime.UtcNow;
                dbAppointmentObj.StartTime = appointment.StartTime;
                dbAppointmentObj.EndTime = appointment.EndTime;
                return Update(dbAppointmentObj);
            }
            return false;
        }
        public Appointment CreateAppointment(Appointment appointment)
        {
            if (appointment != null)
            {
                appointment.CreatedDate = DateTime.UtcNow;
                appointment.UpdatedDate = DateTime.UtcNow;
                appointment.StateId = _stateService.GetStatesByStateNameAndType("Pending", "Appointment").Id;
                Create(appointment);
                return appointment;
            }
            return null;
        }
        public bool UpdateAppointmentState(int Id, int stateId)
        {
            var dbAppointmentObj = Get(Id);
            if (dbAppointmentObj != null)
            {
                dbAppointmentObj.StateId = stateId;
                dbAppointmentObj.UpdatedDate = DateTime.UtcNow;
                return Update(dbAppointmentObj);
            }
            return false;
        }
        public ICollection<Appointment> GetAppointmentsBySaloonAndSates(int saloonId, int[] statesIds)
        {
            return Repository.Table.Where(ap => ap.SaloonId == saloonId && statesIds.Contains(ap.StateId)).ToList();
        }

        public ICollection<Appointment> GetAppointmentsByUserAndSates(int userId, int[] statesIds)
        {
            return Repository.Table.Where(ap => ap.CustomerId == userId && statesIds.Contains(ap.StateId)).ToList();
        }
        public IPagedList<Appointment> GetCurrentUserAppointments(int customerId, bool isFutureAppointments, int pageIndex, int pageSize)
        {
            if (customerId == 0)
                return null;
            IQueryable<Appointment> appointments;
            if (isFutureAppointments)
            {

                appointments = Repository.Table.Where(ap => ap.CustomerId == customerId && ap.Date >= DateTime.UtcNow.Date && !ap.IsDeleted);
                return new PagedList<Appointment>(appointments, pageIndex, pageSize);
            }
            appointments = Repository.Table.Where(ap => ap.CustomerId == customerId && ap.Date <= DateTime.UtcNow.Date && !ap.IsDeleted);
            return new PagedList<Appointment>(appointments, pageIndex, pageSize);
        }

        public bool IsSlotOccupied(int saloonId, int docId, DateTime date, string startTime, string endTime)
        {
            var sTime = TimeSpan.Parse(startTime);
            var eTime = TimeSpan.Parse(endTime);
            var pendingStatusId = _stateService.GetStatesByStateNameAndType("Pending", "Appointment").Id;

            var dbObj = Repository.Table.FirstOrDefault(x => x.StartTime == sTime && x.EndTime == eTime
                                                                                  && x.SaloonId == saloonId &&
                                                                                  x.DoctorId == docId
                                                                                  && x.StateId == pendingStatusId
                                                                                  && x.Date.Day == date.Day);
            return dbObj != null;

        }

        public ICollection<Appointment> GetSaloonsAppointmentsByDate(int[] saloonIds, DateTime dateTime)
        {
            return Repository.Table.Where(x => saloonIds.Contains(x.SaloonId) && x.Date == dateTime).ToList();
        }

        public ICollection<Appointment> GetSaloonsAppointmentsAfterDate(int[] saloonIds, DateTime dateTime)
        {
            return Repository.Table.Where(x => saloonIds.Contains(x.SaloonId) && x.Date >= dateTime).ToList();
        }

        public IPagedList<Appointment> GetAppointmentsApproveForDashboard(int saloonId, int stateId, int pageIndex, int pageSize)
        {
            var query = Repository.Table.Where(x => x.SaloonId == saloonId && x.StateId == stateId);
            return new PagedList<Appointment>(query, pageIndex, pageSize);
        }

        public IPagedList<Appointment> GetAppointmentsbyCompleted(int customerId,int statusId, int pageIndex, int pageSize)
        {
            var query = Repository.Table;
            query = query.Where(ap => ap.CustomerId == customerId && ap.StateId==statusId);
            return new PagedList<Appointment>(query, pageIndex, pageSize);
        }

        public IPagedList<Appointment> GetAppointmentsbyApproved(int customerId,int statusId, int pageIndex, int pageSize)
        {
            var query = Repository.Table;
            query = query.Where(ap => ap.CustomerId == customerId && ap.StateId == statusId);
            return new PagedList<Appointment>(query, pageIndex, pageSize);
        }
        public IPagedList<Appointment> GetAppointmentsbyPending(int customerId,int statusId, int pageIndex, int pageSize)
        {
            var query = Repository.Table;
            query = query.Where(ap => ap.CustomerId == customerId && ap.StateId == statusId);
            return new PagedList<Appointment>(query, pageIndex, pageSize);
        }  
        public IPagedList<Appointment> GetAppointmentsbyReject(int customerId,int statusId, int pageIndex, int pageSize)
        {
            var query = Repository.Table;
            query = query.Where(ap => ap.CustomerId == customerId && ap.StateId == statusId);
            return new PagedList<Appointment>(query, pageIndex, pageSize);
        } 
        public IPagedList<Appointment> GetAppointmentsbyCancel(int customerId,int statusId, int pageIndex, int pageSize)
        {
            var query = Repository.Table;
            query = query.Where(ap => ap.CustomerId == customerId && ap.StateId == statusId);
            return new PagedList<Appointment>(query, pageIndex, pageSize);
        }

        public IPagedList<Appointment> GetAppointmentsActiveForAdmin(int statusId, int pageIndex, int pageSize)
        {
            var query = Repository.Table;
            query = query.Where(ap =>  ap.StateId == statusId);
            return new PagedList<Appointment>(query, pageIndex, pageSize);
        }

        public IPagedList<Appointment> GetAppointmentsApprovedForAdmin(int statusId, int pageIndex, int pageSize)
        {
            var query = Repository.Table;
            query = query.Where(ap => ap.StateId == statusId);
            return new PagedList<Appointment>(query, pageIndex, pageSize);
        }  
        public IPagedList<Appointment> GetAppointmentsPendingForAdmin(int statusId, int pageIndex, int pageSize)
        {
            var query = Repository.Table;
            query = query.Where(ap => ap.StateId == statusId);
            return new PagedList<Appointment>(query, pageIndex, pageSize);
        }    public IPagedList<Appointment> GetAppointmentsRejectForAdmin(int statusId, int pageIndex, int pageSize)
        {
            var query = Repository.Table;
            query = query.Where(ap => ap.StateId == statusId);
            return new PagedList<Appointment>(query, pageIndex, pageSize);
        }    public IPagedList<Appointment> GetAppointmentsCancelForAdmin(int statusId, int pageIndex, int pageSize)
        {
            var query = Repository.Table;
            query = query.Where(ap => ap.StateId == statusId);
            return new PagedList<Appointment>(query, pageIndex, pageSize);
        }

        public List<Appointment> GetAppointmentsApproveForLab(int userId,int saloonId, int statusId)
        {
            var query = Repository.Table;
            List<Appointment> List = new List<Appointment>();

            var Users = _laboratoryUsersService.GetLaboratoryUserById(userId);
            foreach (var user in Users)
            {
                var labs = _saloonLaboratoryService.GetSaloonLaboratoriesByLaboratoryId(user.LaboratoryId);
                //foreach (var item in labs)
                //{
                //    item.Saloon = _saloonService.Get(item.SaloonId);
                //}

                foreach (var lab in labs)
                {
                    query = query.Where(x => x.SaloonId == lab.SaloonId && x.StateId == statusId);
                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            List.Add(new Appointment()
                            {
                                CreatedBy = item.CreatedBy,
                                CreatedDate = item.CreatedDate,
                                CustomerId = item.CustomerId,
                                Date = item.Date,
                                DoctorId = item.DoctorId,
                                EndTime = item.EndTime,
                                Id = item.Id,
                                SaloonId = item.SaloonId,
                                StartTime = item.StartTime,
                                StateId = item.StateId,

                            });
                        }
                     
                    }
                    query = Repository.Table;
                }
            }

            return List;
        }
        
        public List<Appointment> GetAppointmentPendingForLab(int userId,int saloonId, int statusId)
        {
            var query = Repository.Table;
            List<Appointment> List = new List<Appointment>();

            var Users = _laboratoryUsersService.GetLaboratoryUserById(userId);
            foreach (var user in Users)
            {
                var labs = _saloonLaboratoryService.GetSaloonLaboratoriesByLaboratoryId(user.LaboratoryId);
                //foreach (var item in labs)
                //{
                //    item.Saloon = _saloonService.Get(item.SaloonId);
                //}

                foreach (var lab in labs)
                {
                    query = query.Where(x => x.SaloonId == lab.SaloonId && x.StateId == statusId);
                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            List.Add(new Appointment()
                            {
                                CreatedBy = item.CreatedBy,
                                CreatedDate = item.CreatedDate,
                                CustomerId = item.CustomerId,
                                Date = item.Date,
                                DoctorId = item.DoctorId,
                                EndTime = item.EndTime,
                                Id = item.Id,
                                SaloonId = item.SaloonId,
                                StartTime = item.StartTime,
                                StateId = item.StateId,

                            });
                        }
                     
                    }
                    query = Repository.Table;
                }
            }

            return List;
        }

        public List<Appointment> GetAppointmentsRejectForLab(int userId, int saloonId, int statusId)
        {
            var query = Repository.Table;
            List<Appointment> List = new List<Appointment>();

            var Users = _laboratoryUsersService.GetLaboratoryUserById(userId);
            foreach (var user in Users)
            {
                var labs = _saloonLaboratoryService.GetSaloonLaboratoriesByLaboratoryId(user.LaboratoryId);
                //foreach (var item in labs)
                //{
                //    item.Saloon = _saloonService.Get(item.SaloonId);
                //}

                foreach (var lab in labs)
                {
                    query = query.Where(x => x.SaloonId == lab.SaloonId && x.StateId == statusId);
                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            List.Add(new Appointment()
                            {
                                CreatedBy = item.CreatedBy,
                                CreatedDate = item.CreatedDate,
                                CustomerId = item.CustomerId,
                                Date = item.Date,
                                DoctorId = item.DoctorId,
                                EndTime = item.EndTime,
                                Id = item.Id,
                                SaloonId = item.SaloonId,
                                StartTime = item.StartTime,
                                StateId = item.StateId,

                            });
                        }

                    }
                    query = Repository.Table;
                }
            }

            return List;
        }

        public List<Appointment> GetAppointmentsCancelForLab(int userId, int saloonId, int statusId)
        {
            var query = Repository.Table;
            List<Appointment> List = new List<Appointment>();

            var Users = _laboratoryUsersService.GetLaboratoryUserById(userId);
            foreach (var user in Users)
            {
                var labs = _saloonLaboratoryService.GetSaloonLaboratoriesByLaboratoryId(user.LaboratoryId);
                //foreach (var item in labs)
                //{
                //    item.Saloon = _saloonService.Get(item.SaloonId);
                //}

                foreach (var lab in labs)
                {
                    query = query.Where(x => x.SaloonId == lab.SaloonId && x.StateId == statusId);
                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            List.Add(new Appointment()
                            {
                                CreatedBy = item.CreatedBy,
                                CreatedDate = item.CreatedDate,
                                CustomerId = item.CustomerId,
                                Date = item.Date,
                                DoctorId = item.DoctorId,
                                EndTime = item.EndTime,
                                Id = item.Id,
                                SaloonId = item.SaloonId,
                                StartTime = item.StartTime,
                                StateId = item.StateId,

                            });
                        }

                    }
                    query = Repository.Table;
                }
            }

            return List;
        }

        public List<Appointment> GetAppointmentsActiveForLabApprove(int userId, int saloonId, int statusId)
        {
            var query = Repository.Table;
            List<Appointment> List = new List<Appointment>();

            var Users = _laboratoryUsersService.GetLaboratoryUserById(userId);
            foreach (var user in Users)
            {
                var labs = _saloonLaboratoryService.GetSaloonLaboratoriesByLaboratoryId(user.LaboratoryId);
                //foreach (var item in labs)
                //{
                //    item.Saloon = _saloonService.Get(item.SaloonId);
                //}

                foreach (var lab in labs)
                {
                    query = query.Where(x => x.SaloonId == lab.SaloonId && x.StateId == statusId);
                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            List.Add(new Appointment()
                            {
                                CreatedBy = item.CreatedBy,
                                CreatedDate = item.CreatedDate,
                                CustomerId = item.CustomerId,
                                Date = item.Date,
                                DoctorId = item.DoctorId,
                                EndTime = item.EndTime,
                                Id = item.Id,
                                SaloonId = item.SaloonId,
                                StartTime = item.StartTime,
                                StateId = item.StateId,

                            });
                        }

                    }
                    query = Repository.Table;
                }
            }

            return List;
        }

        public IPagedList<Appointment> GetAppointmentsApprovebyDoctorId(int doctorId, int statusId, int pageIndex, int pageSize)
        {
            var query = Repository.Table;
            query = query.Where(ap => ap.DoctorId == doctorId && !ap.IsDeleted && ap.StateId== statusId);
            return new PagedList<Appointment>(query, pageIndex, pageSize);
        }       
       
        public IPagedList<Appointment> GetAppointmentsPendingbyDoctorId(int doctorId, int statusId, int pageIndex, int pageSize)
        {
            var query = Repository.Table;
            query = query.Where(ap => ap.DoctorId == doctorId && !ap.IsDeleted && ap.StateId== statusId);
            return new PagedList<Appointment>(query, pageIndex, pageSize);
        }    
        public IPagedList<Appointment> GetAppointmentsRejcetbyDoctorId(int doctorId, int statusId, int pageIndex, int pageSize)
        {
            var query = Repository.Table;
            query = query.Where(ap => ap.DoctorId == doctorId && !ap.IsDeleted && ap.StateId== statusId);
            return new PagedList<Appointment>(query, pageIndex, pageSize);
        }      
        public IPagedList<Appointment> GetAppointmentsCancelbyDoctorId(int doctorId, int statusId, int pageIndex, int pageSize)
        {
            var query = Repository.Table;
            query = query.Where(ap => ap.DoctorId == doctorId && !ap.IsDeleted && ap.StateId== statusId);
            return new PagedList<Appointment>(query, pageIndex, pageSize);
        }

     

        //public IPagedList<Appointment> GetAppointmentsPendingbyDoctorId(int doctorId, int statusId, int pageIndex, int pageSize)
        //{
        //    var query = Repository.Table;
        //    query = query.Where(ap => ap.DoctorId == doctorId && !ap.IsDeleted && ap.StateId == statusId);
        //    return new PagedList<Appointment>(query, pageIndex, pageSize);
        //}

        public IPagedList<Appointment> GetAppointmentsRejectbyDoctorId(int doctorId, int statusId, int pageIndex, int pageSize)
        {
            var query = Repository.Table;
            query = query.Where(ap => ap.DoctorId == doctorId && !ap.IsDeleted && ap.StateId == statusId);
            return new PagedList<Appointment>(query, pageIndex, pageSize);
        }

        //public IPagedList<Appointment> GetAppointmentsCancelbyDoctorId(int doctorId, int statusId, int pageIndex, int pageSize)
        //{
        //    var query = Repository.Table;
        //    query = query.Where(ap => ap.DoctorId == doctorId && !ap.IsDeleted && ap.StateId == statusId);
        //    return new PagedList<Appointment>(query, pageIndex, pageSize);
        //}
        #endregion
    }
}
