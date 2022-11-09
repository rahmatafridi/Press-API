namespace PressDot.Contracts.Request.Saloon
{
    public class SaloonEmployeeCreateRequest : BasePressDotEntityModel
    {
        public int SaloonId { get; set; }

        public int EmployeeId { get; set; }
    }
}
