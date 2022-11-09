using PressDot.Contracts;
using PressDot.Contracts.Response.Appointment;
using PressDot.Contracts.Request.Appointment;
using System;
using System.Collections.Generic;
using PressDot.Core.Domain;

namespace PressDot.Facade.Infrastructure
{
    public interface IAppointmentFacade
    {
        PressDotPageListEntityModel<AppointmentResponse> GetAppointmentbySaloonId(int Id, int pageIndex, int pageSize);
        PressDotPageListEntityModel<AppointmentResponse> GetAppointmentbySaloonId(int Id, DateTime date, int pageIndex, int pageSize);
        PressDotPageListEntityModel<AppointmentResponse> GetAppointmentbyDoctorId(int Id, int pageIndex, int pageSize);
        PressDotPageListEntityModel<AppointmentResponse> GetAppointmentApprovedbyDoctorId(int Id,int statusId, int pageIndex, int pageSize);
        PressDotPageListEntityModel<AppointmentResponse> GetAppointmentActivebyDoctorId(int Id,int statusId, int pageIndex, int pageSize);
        PressDotPageListEntityModel<AppointmentResponse> GetAppointmentPendingbyDoctorId(int Id,int statusId, int pageIndex, int pageSize);
        PressDotPageListEntityModel<AppointmentResponse> GetAppointmentRejectbyDoctorId(int Id,int statusId, int pageIndex, int pageSize);
        PressDotPageListEntityModel<AppointmentResponse> GetAppointmentCancelbyDoctorId(int Id,int statusId, int pageIndex, int pageSize);
        PressDotPageListEntityModel<AppointmentResponse> GetAppointmentbyCustomerId(int Id, int pageIndex, int pageSize);

        PressDotPageListEntityModel<AppointmentResponse> GetAppointmentbySaloonAdministratorId(int saloonAdministatorId,int stateId, int pageIndex, int pageSize);
        PressDotPageListEntityModel<AppointmentResponse> GetAppointmentsbyAppointmentStates(AppointmentStateRequest request, int pageIndex, int pageSize);

        PressDotPageListEntityModel<AppointmentResponse> GetSaloonAppointmentsForSaloonAdministrator(
            AppointmentStateRequest request, int userId, int pageIndex, int pageSize);

        PressDotPageListEntityModel<CurrentUserAppointmentResponse> GetCurrentUserAppointments(int customerId,
            bool isFutureAppointments, int pageIndex, int pageSize);
        AppointmentResponse GetAppointmentById(int id);
        bool UpdateAppointment(AppointmentRequest request);
        bool UpdateAppointmentState(int Id, int stateId);
        bool CreateAppointment(AppointmentRequest request);
        PressDotPageListEntityModel<AppointmentResponse> GetCompleteAppointmentForDashboard(int userId, int stateId, int pageIndex, int pageSize);
        PressDotPageListEntityModel<AppointmentResponse> GetApprovedAppointmentForDashboard(int userId, int stateId,string search, int pageIndex, int pageSize);
        PressDotPageListEntityModel<AppointmentResponse> GetPendingAppointmentForDashboard(int userId, int stateId, int pageIndex, int pageSize);
        PressDotPageListEntityModel<AppointmentResponse> GetRejectAppointmentForDashboard(int userId, int stateId, int pageIndex, int pageSize);
        PressDotPageListEntityModel<AppointmentResponse> GetCancelAppointmentForDashboard(int userId, int stateId, int pageIndex, int pageSize);
        List<ProducatResponse> LoadProducts();
        List<Appointment> GetPendingAppointmentForLab(int userId, int stateId, int pageIndex, int pageSize);
        List<Appointment> GetApprovedAppointmentForLab(int userId, int stateId, int pageIndex, int pageSize);
        List<Appointment> GetRejectAppointmentForLab(int userId, int stateId, int pageIndex, int pageSize);
        List<Appointment> GetCancelAppointmentForLab(int userId, int stateId, int pageIndex, int pageSize);
        List<Appointment> GetActiveAppointmentForLabApprove(int userId, int stateId, int pageIndex, int pageSize);

    }
}
