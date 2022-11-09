using System;
using System.Collections.Generic;
using System.Text;

namespace PressDot.Core.Exceptions
{
    public class PressDotValidationException : Exception
    {
        public int Code { get; set; }
        public PressDotValidationException()
        {

        }

        public PressDotValidationException(string message) : base(message)
        {

        }

        public PressDotValidationException(int code, string message) : base(message)
        {
            Code = code;
        }
    }
}
