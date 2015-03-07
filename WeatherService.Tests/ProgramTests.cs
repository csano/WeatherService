using Microsoft.VisualStudio.TestTools.UnitTesting;
using WpfApplication1;

namespace WeatherService.Tests
{
    [TestClass]
    public class ProgramTests
    {
        [TestMethod]
        public void CanCreateView()
        {
            var view = Program.CreateView();
            Assert.IsNotNull(view);
        }
    }
}
