using System;
using System.Collections.Generic;
using System.Text;

namespace PressDot.Core.Exceptions
{
    public class PressDotException : Exception
    {
        public int Code { get; set; }
        public PressDotException()
        {

        }

        public PressDotException(string message) : base(message)
        {

        }

        public PressDotException(int code, string message) : base(message)
        {
            Code = code;
        }
    }
}
