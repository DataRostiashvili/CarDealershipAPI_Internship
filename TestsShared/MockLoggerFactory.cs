using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Application.Logger;

namespace TestsShared
{
    public static class MockLoggerFactory
    {
        public static Mock<ILoggerAdapter<T>> Create<T>()
        {
            var mockLogger = new Mock<ILoggerAdapter<T>>();

            mockLogger.Setup(logger => logger.LogInformation(It.IsAny<string>()));
            mockLogger.Setup(logger => logger.LogError(It.IsAny<Exception>(), It.IsAny<string>()));

            return mockLogger;
        }
    }
}
