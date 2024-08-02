﻿using Moq;
using Stryker.Configuration.Testing;
using System.Collections.Generic;
using System.IO;

namespace Stryker.Configuration.UnitTest
{
    public static class MockExtensions
    {
        public static void SetupProcessMockToReturn(this Mock<IProcessExecutor> processExecutorMock, string result, int exitCode = 0)
        {
            processExecutorMock.Setup(x => x.Start(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<IEnumerable<KeyValuePair<string, string>>>(),
                It.IsAny<int>()))
            .Returns(new ProcessResult()
            {
                ExitCode = exitCode,
                Output = result.Replace('\\', Path.DirectorySeparatorChar)
            });
        }
    }
}
