using PressDot.Contracts.Response.State;
using System;

namespace PressDot.Contracts.Response.Order
{
    public class OrderResponseForLaboratoryUserId : BasePressDotEntityModel
    {
        public int Id { get; set; }
        public int AppointmentId { get; set; }
        public int LaboratoryId { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedDateString { get; set; }
        public int StateId { get; set; }
        public string DoctorFirstName { get; set; }
        public string DoctorLastName { get; set; }
        public string SaloonName { get; set; }
        public StateResponse State { get; set; }
    }
}
