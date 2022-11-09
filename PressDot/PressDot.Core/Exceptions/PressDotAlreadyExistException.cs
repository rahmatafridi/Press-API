using System;
using System.Collections.Generic;
using System.Text;

namespace PressDot.Core.Exceptions
{
    public class PressDotAlreadyExistException : Exception
    {
        public int Code { get; set; }
        public PressDotAlreadyExistException()
        {

        }

        public PressDotAlreadyExistException(string message) : base(message)
        {

        }

        public PressDotAlreadyExistException(int code, string message) : base(message)
        {
            Code = code;
        }
    }
}
