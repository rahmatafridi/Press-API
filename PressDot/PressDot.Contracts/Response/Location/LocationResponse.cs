using System;
using System.Collections.Generic;
using System.Text;

namespace PressDot.Contracts.Response.Location
{
    public class LocationResponse : BasePressDotEntityModel
    {
        public int Id { get; set; }

        public string LocationName { get; set; }

        public int? ParentLocationId { get; set; }
    }
}
