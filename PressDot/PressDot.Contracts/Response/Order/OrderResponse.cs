using PressDot.Contracts.Response.Appointment;
using PressDot.Contracts.Response.Laboratory;
using PressDot.Contracts.Response.State;

namespace PressDot.Contracts.Response.Order
{
    public class OrderResponse : BasePressDotEntityModel
    {
        public int Id { get; set; }
        public int AppointmentId { get; set; }
        public int LaboratoryId { get; set; }
        public string Description { get; set; }
        public int StateId { get; set; }
        public AppointmentResponse Appointment { get; set; }
        public LaboratoryResponse Laboratory { get; set; }
        public StateResponse State { get; set; }
    }
}
