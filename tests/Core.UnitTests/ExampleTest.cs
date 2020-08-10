using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;

namespace Core.UnitTests
{
    [TestClass]
    public class ExampleTest
    {
        [TestMethod]
        public void TestThatItWorks()
        {
            true.Should().Be(true);
        }
    }
}
