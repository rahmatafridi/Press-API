using System;

namespace PressDot.Core.Domain
{
    public class Appointment : BaseEntity
    {
        public int CustomerId { get; set; }
        public int DoctorId { get; set; }
        public int SaloonId { get; set; }
        public string Symptoms { get; set; }
        public int StateId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public Users Customer { get; set; }
        public Saloon Saloon { get; set; }
        public Users Doctor { get; set; }
        public States State { get; set; }

    }
}
