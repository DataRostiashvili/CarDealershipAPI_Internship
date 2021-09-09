using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Logger
{
    public interface ILoggerAdapter<T>
    {
        void LogInformation(string message);
    }
}
