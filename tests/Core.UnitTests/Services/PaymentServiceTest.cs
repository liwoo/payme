using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;

namespace Core.UnitTests.Services
{
    [TestClass]
    public class PaymentServiceTest
    {
        private readonly PaymentService service;
        public PaymentServiceTest()
        {
            this.service = new PaymentService();
        }

        [TestMethod]
        public void TestThatItWorks()
        {
            true.Should().Be(true);
        }

    }
}