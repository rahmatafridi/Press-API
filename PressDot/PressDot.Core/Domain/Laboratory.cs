using System;
using System.Collections.Generic;
using System.Text;

namespace PressDot.Core.Domain
{
    public class Laboratory : BaseEntity
    {
        public string LaboratoryName { get; set; }

        public virtual ICollection<SaloonLaboratory> SaloonLaboratories { get; set; }
    }
}
