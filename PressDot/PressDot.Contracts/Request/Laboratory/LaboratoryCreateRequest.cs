using System;
using System.Collections.Generic;
using System.Text;

namespace PressDot.Contracts.Request.Laboratory
{
    public class LaboratoryCreateRequest : BasePressDotEntityModel
    {
        public string LaboratoryName { get; set; }
    }
}
