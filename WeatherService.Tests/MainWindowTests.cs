using ApprovalTests.Wpf;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WpfApplication1;

namespace WeatherService.Tests
{
    [TestClass]
    public class MainWindowTests
    {
        [TestMethod]
        [TestCategory("UI")]
        public void BindsToViewModelWithoutError()
        {
            WpfBindingsAssert.BindsWithoutError(new WeatherViewModel(), () => new MainWindow());
        }

        [TestMethod]
        [TestCategory("UI")]
        //[UseReporter(typeof(DiffReporter))]
        public void AppearanceOfMainWindow()
        {
            var testSubject = new MainWindow
            {
                DataContext = new WeatherViewModel
                {
                    Location = new Location {City = "Here!"},
                    Temperature = 98.6
                }
            };
            WpfApprovals.Verify(testSubject);
        }
    }
}
