using System;
using System.Collections.Generic;
using System.Text;

namespace PressDot.Contracts.Response.Saloon
{
    public class SaloonTypeResponse: BasePressDotEntityModel
    {
        public int Id { get; set; }
        public string SaloonTypeName { get; set; }
    }
}
