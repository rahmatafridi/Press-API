using System;
using System.Collections.Generic;

namespace PressDot.Contracts.Response.Saloon
{
    public class GetSaloonAppointmentsDto
    {
        public int SaloonEmployeeScheduleId { get; set; }
        public string SaloonName { get; set; }
        public string DoctorName { get; set; }
        public DateTime Date { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public int DoctorId { get; set; }
        public int SaloonId { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public List<SlotDto> Slots { get; set; }
    }
}
