using Moq;
using Stryker.Configuration.Mutants;
using Stryker.Configuration.Reporters.Progress;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Stryker.Configuration.UnitTest.Reporters.Progress
{
    [TestClass]
    public class ProgressReporterTests : TestBase
    {
        private readonly Mock<IProgressBarReporter> _progressBarReporter;

        private readonly ProgressReporter _progressReporter;

        public ProgressReporterTests()
        {
            _progressBarReporter = new Mock<IProgressBarReporter>();

            _progressReporter = new ProgressReporter(_progressBarReporter.Object);
        }

        [TestMethod]
        public void ProgressReporter_ShouldCallBothReporters_OnReportInitialState()
        {
            var mutants = new Mutant[3] { new Mutant(), new Mutant(), new Mutant() };

            _progressReporter.OnStartMutantTestRun(mutants);
            _progressBarReporter.Verify(x => x.ReportInitialState(mutants.Length), Times.Once);
        }

        [TestMethod]
        public void ProgressReporter_ShouldCallBothReporters_OnReportRunTest()
        {
            var mutant = new Mutant();
            _progressReporter.OnMutantTested(mutant);

            _progressBarReporter.Verify(x => x.ReportRunTest(mutant), Times.Once);
        }
    }
}
