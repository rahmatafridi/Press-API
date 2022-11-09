namespace PressDot.Contracts.Request.LaboratoryUsers
{
    public class CreateLaboratoryUsersRequest : BasePressDotEntityModel
    {
        public int LaboratoryId { get; set; }
        public int UserId { get; set; }
        public bool IsEmailReceiver { get; set; }
    }
}
