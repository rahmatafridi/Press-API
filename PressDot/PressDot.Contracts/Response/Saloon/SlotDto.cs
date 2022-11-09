using System;
using System.Collections.Generic;
using System.Text;

namespace PressDot.Contracts.Response.Saloon
{
    public class SlotDto
    {
        public string StartHour { get; set; }
        public string EndHour { get; set; }
        public bool IsOccupied { get; set; }
    }
}
