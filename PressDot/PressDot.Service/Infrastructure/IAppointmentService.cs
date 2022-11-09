using PressDot.Core;
using PressDot.Core.Domain;
using System;
using System.Collections.Generic;

namespace PressDot.Service.Infrastructure
{
    public interface IAppointmentService : IService<Appointment>
    {
        IPagedList<Appointment> GetAppointmentsbySaloonId(int saloonId, int pageIndex, int pageSize);

        IPagedList<Appointment> GetAppointmentsbySaloonIdAndState(int saloonId, int stateId, int pageIndex, int pageSize);
        IPagedList<Appointment> GetAppointmentsApproveForDashboard(int saloonId, int stateId, int pageIndex, int pageSize);

        IPagedList<Appointment> GetAppointmentsbySaloonId(int saloonId, DateTime date, int pageIndex, int pageSize);
        IPagedList<Appointment> GetAppointmentsbyDoctorId(int doctorId, int pageIndex, int pageSize);
        IPagedList<Appointment> GetAppointmentsPendingbyDoctorId(int doctorId,int statusId, int pageIndex, int pageSize);
        IPagedList<Appointment> GetAppointmentsApprovebyDoctorId(int doctorId,int statusId, int pageIndex, int pageSize);
        IPagedList<Appointment> GetAppointmentsRejectbyDoctorId(int doctorId,int statusId, int pageIndex, int pageSize);
        IPagedList<Appointment> GetAppointmentsCancelbyDoctorId(int doctorId,int statusId, int pageIndex, int pageSize);

        IPagedList<Appointment> GetAppointmentsbyCustomerId(int customerId, int pageIndex, int pageSize);
        IPagedList<Appointment> GetAppointmentsbyCompleted(int customerId,int statusId, int pageIndex, int pageSize);
        IPagedList<Appointment> GetAppointmentsActiveForAdmin(int statusId, int pageIndex, int pageSize);
         List<Appointment> GetAppointmentsApproveForLab(int userId,int saloonId, int statusId);
         List<Appointment> GetAppointmentPendingForLab(int userId,int saloonId, int statusId);
         List<Appointment> GetAppointmentsRejectForLab(int userId,int saloonId, int statusId);
         List<Appointment> GetAppointmentsCancelForLab(int userId,int saloonId, int statusId);
        List<Appointment> GetAppointmentsActiveForLabApprove(int userId, int saloonId, int statusId);

        IPagedList<Appointment> GetAppointmentsbyPending(int customerId,int statusId, int pageIndex, int pageSize);
        IPagedList<Appointment> GetAppointmentsbyReject(int customerId,int statusId, int pageIndex, int pageSize);
        IPagedList<Appointment> GetAppointmentsbyCancel(int customerId,int statusId, int pageIndex, int pageSize);
        IPagedList<Appointment> GetAppointmentsbyApproved(int customerId,int statusId, int pageIndex, int pageSize);
        IPagedList<Appointment> GetAppointmentsApprovedForAdmin(int statusId, int pageIndex, int pageSize);
        IPagedList<Appointment> GetAppointmentsPendingForAdmin(int statusId, int pageIndex, int pageSize);
        IPagedList<Appointment> GetAppointmentsRejectForAdmin(int statusId, int pageIndex, int pageSize);
        IPagedList<Appointment> GetAppointmentsCancelForAdmin(int statusId, int pageIndex, int pageSize);
        
        IPagedList<Appointment> GetAppointmentsbyAppointmentState(List<int> stateIds, int pageIndex, int pageSize);

        IPagedList<Appointment> GetSaloonAppointmentsForSaloonAdministrator(List<int> stateIds, int saloonId,
            int pageIndex, int pageSize);

        List<Appointment> GetAppointmentsBySaloonId(int saloonId);
        List<Appointment> GetAppointmentsbyDoctorId(int doctorId);
        bool UpdateAppointment(Appointment appointment);
        bool UpdateAppointmentState(int Id, int stateId);
        Appointment CreateAppointment(Appointment appointment);
        /// <summary>
        /// Get Appointments for specific saloon with specific states
        /// </summary>
        /// <param name="saloonId"></param>
        /// <param name="statesIds"></param>
        /// <returns></returns>
        ICollection<Appointment> GetAppointmentsBySaloonAndSates(int saloonId, int[] statesIds);
        /// <summary>
        /// Get Appointments for specific user with specific states
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="statesIds"></param>
        /// <returns></returns>
        ICollection<Appointment> GetAppointmentsByUserAndSates(int userId, int[] statesIds);

        IPagedList<Appointment> GetCurrentUserAppointments(int customerId, bool isFutureAppointments, int pageIndex, int pageSize);

        bool IsSlotOccupied(int saloonId, int docId, DateTime date, string startTime, string endTime);

        /// <summary>
        /// Get appointments of saloons for specific date
        /// </summary>
        /// <param name="saloonIds">Array of saloon id</param>
        /// <param name="dateTime">date after which appointments will fetched</param>
        /// <returns></returns>
        ICollection<Appointment> GetSaloonsAppointmentsByDate(int[] saloonIds, DateTime dateTime);

        /// <summary>
        /// Get appointments of saloons after or equal to specific date 
        /// </summary>
        /// <param name="saloonIds"></param>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        ICollection<Appointment> GetSaloonsAppointmentsAfterDate(int[] saloonIds, DateTime dateTime);
    }
}
