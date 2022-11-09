using System;

namespace PressDot.Contracts.Request.Appointment
{
    public class AppointmentRequest : BasePressDotEntityModel
    {
        public int Id { get; set; }
        public int? CustomerId { get; set; }
        public int? DoctorId { get; set; }
        public int? SaloonId { get; set; }
        public string Symptoms { get; set; }
        public int? StateId { get; set; }
        public DateTime? Date { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }
}
