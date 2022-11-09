namespace PressDot.Core.Domain
{
    public class SaloonLaboratory : BaseEntity
    {
        public int SaloonId { get; set; }

        public int LaboratoryId { get; set; }
        public bool IsDefault { get; set; }
        public virtual Saloon Saloon { get; set; }

        public virtual Laboratory Laboratory { get; set; }
    }
}
