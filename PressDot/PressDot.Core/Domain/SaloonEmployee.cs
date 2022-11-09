namespace PressDot.Core.Domain
{
    public class SaloonEmployee : BaseEntity
    {
        public int SaloonId { get; set; }

        public int EmployeeId { get; set; }

        public virtual Saloon Saloon { get; set; }

        public virtual Users Employee { get; set; }
    }
}
