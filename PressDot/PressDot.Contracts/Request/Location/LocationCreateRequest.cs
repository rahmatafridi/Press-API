using System;
using System.Collections.Generic;
using System.Text;

namespace PressDot.Contracts.Request.Location
{
    public class LocationCreateRequest : BasePressDotEntityModel
    {
        public string LocationName { get; set; }

        public int? ParentLocationId { get; set; }
    }
}
