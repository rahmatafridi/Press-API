namespace PressDot.Contracts.Request.Order
{
    public class UpdateOrderRequest : BasePressDotEntityModel
    {
        public int Id { get; set; }
        public int AppointmentId { get; set; }
        public int LaboratoryId { get; set; }
        public string Description { get; set; }
        public int StateId { get; set; }
    }
}
