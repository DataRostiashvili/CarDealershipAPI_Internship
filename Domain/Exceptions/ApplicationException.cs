﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class ApplicationException : Exception
    {
        public ApplicationException(string message, Exception innerException = null)
            : base(message, innerException) { }
    }
}
