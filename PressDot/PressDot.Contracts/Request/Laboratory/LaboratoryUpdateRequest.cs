using System;
using System.Collections.Generic;
using System.Text;

namespace PressDot.Contracts.Request.Laboratory
{
    public class LaboratoryUpdateRequest : BasePressDotEntityModel
    {
        public int Id { get; set; }
        public string LaboratoryName { get; set; }
    }
}
