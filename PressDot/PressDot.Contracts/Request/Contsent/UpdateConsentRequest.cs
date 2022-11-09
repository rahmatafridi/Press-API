using System;
using System.Collections.Generic;
using System.Text;

namespace PressDot.Contracts.Request.Contsent
{
    public  class UpdateConsentRequest
    {
        public int Id { get; set; } 
        public int UserId { get; set; } 
        public string note { get; set; }
    }
}
