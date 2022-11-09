namespace PressDot.Contracts.Response.SaloonLaboratory
{
    public class SaloonLaboratoryResponse : BasePressDotEntityModel
    {
        public int Id { get; set; }
        public int SaloonId { get; set; }

        public int LaboratoryId { get; set; }
        public string LaboratoryName { get; set; }
        public bool IsDefault { get; set; }
    }
}
