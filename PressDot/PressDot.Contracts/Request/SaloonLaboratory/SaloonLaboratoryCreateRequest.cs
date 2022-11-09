using System.ComponentModel.DataAnnotations;

namespace PressDot.Contracts.Request.SaloonLaboratory
{
    public class SaloonLaboratoryCreateRequest : BasePressDotEntityModel
    {
        public int SaloonId { get; set; }

        public int LaboratoryId { get; set; }
        [Required]
        public bool IsDefault { get; set; }
    }
}
