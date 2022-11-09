namespace PressDot.Core.Domain
{
    public class Order : BaseEntity
    {
        public int AppointmentId { get; set; }
        public int LaboratoryId { get; set; }
        public string Description { get; set; }
        public int StateId { get; set; }
        public Appointment Appointment { get; set; }
        public Laboratory Laboratory { get; set; }
        public States State { get; set; }

    }
}
