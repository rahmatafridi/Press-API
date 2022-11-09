using System;
using System.Collections.Generic;
using System.Text;

namespace PressDot.Core.Domain
{
    public class Location : BaseEntity
    {
        public string LocationName { get; set; }

        public int? ParentLocationId { get; set; }

        public virtual Location ParentLocation { get; set; }
    }
}
