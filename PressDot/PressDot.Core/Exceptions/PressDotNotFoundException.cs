using System;
using System.Collections.Generic;
using System.Text;

namespace PressDot.Core.Exceptions
{
    public class PressDotNotFoundException : Exception
    {
        public int Code { get; set; }
        public PressDotNotFoundException()
        {

        }

        public PressDotNotFoundException(string message) : base(message)
        {

        }

        public PressDotNotFoundException(int code, string message) : base(message)
        {
            Code = code;
        }
    }
}
