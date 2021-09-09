using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
        public class ClientAlreadyExistsException : ApplicationException
        {
            public ClientAlreadyExistsException(string message, Exception innerException = null)
              : base(message, innerException) { }
        public ClientAlreadyExistsException()
        { }
    }
    
}
