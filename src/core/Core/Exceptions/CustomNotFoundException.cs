﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Exceptions
{
    public class CustomNotFoundException : Exception
    {
        public CustomNotFoundException()
        {
        }

        public CustomNotFoundException(string message)
            : base(message)
        {
        }

        public CustomNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
