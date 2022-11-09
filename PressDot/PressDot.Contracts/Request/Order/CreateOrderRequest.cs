namespace PressDot.Contracts.Request.Order
{
    public class CreateOrderRequest : BasePressDotEntityModel
    {
        public int AppointmentId { get; set; }
        public string Description { get; set; }
    }
}
