using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class CarAlreadyRegisteredForClientException : ApplicationException
    {
        public CarAlreadyRegisteredForClientException(string message, Exception innerException = null)
         : base(message, innerException) { }
        public CarAlreadyRegisteredForClientException()
        { }
    }
}
