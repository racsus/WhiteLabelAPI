using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Exceptions
{
    public class CustomUnauthorizedException: Exception
    {
        public CustomUnauthorizedException()
        {
        }

        public CustomUnauthorizedException(string message)
            : base(message)
        {
        }

        public CustomUnauthorizedException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
