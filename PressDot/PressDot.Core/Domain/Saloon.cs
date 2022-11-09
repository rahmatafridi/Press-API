using System.Collections.Generic;

namespace PressDot.Core.Domain
{
    public class Saloon : BaseEntity
    {
        public string SaloonName { get; set; }

        public int CountryId { get; set; }

        public int CityId { get; set; }

        public int SaloonTypeId { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

        public SaloonType SaloonType { get; set; }

        public virtual Location Country { get; set; }

        public virtual Location City { get; set; }

        public virtual ICollection<SaloonEmployee> SaloonEmployees { get; set; }

        public virtual ICollection<SaloonLaboratory> SaloonLaboratories { get; set; }
    }
}
