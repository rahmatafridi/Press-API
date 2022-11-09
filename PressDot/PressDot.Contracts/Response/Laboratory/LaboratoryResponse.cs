using System;
using System.Collections.Generic;
using System.Text;

namespace PressDot.Contracts.Response.Laboratory
{
    public class LaboratoryResponse : BasePressDotEntityModel
    {
        public int Id { get; set; }

        public string LaboratoryName { get; set; }
    }
}
