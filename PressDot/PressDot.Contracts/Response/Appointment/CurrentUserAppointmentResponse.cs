using PressDot.Contracts.Response.Saloon;
using PressDot.Contracts.Response.State;
using PressDot.Contracts.Response.Users;
using System;

namespace PressDot.Contracts.Response.Appointment
{
    public class CurrentUserAppointmentResponse : BasePressDotEntityModel
    {
        public int Id { get; set; }
        public int? CustomerId { get; set; }
        public int? DoctorId { get; set; }
        public string DoctorFirstName { get; set; }
        public string DoctorLastName { get; set; }
        public int? SaloonId { get; set; }
        public string SaloonName { get; set; }
        public string SaloonEmail { get; set; }
        public string SaloonPhone { get; set; }
        public string SaloonAddress { get; set; }
        public string Symptoms { get; set; }
        public int? StateId { get; set; }
        public StateResponse State { get; set; }
        public DateTime? Date { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public string StartTimeString { get; set; }
        public string EndTimeString { get; set; }

        public bool CanCancel { get; set; }
    }
}
