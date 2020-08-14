using System;
using Core.Services;
using FluentAssertions;
using Xunit;

namespace Domain.UnitTests.Services
{
    public class PaymentServiceTest
    {
        private readonly PaymentService _service;

        public PaymentServiceTest()
        {
            _service = new PaymentService();
        }

        [Fact]
        public void PaymentService_ShouldWork()
        {
            true.Should().BeTrue();
        }

        [Fact]
        public void PaymentService_ShouldGenerateMpambaPaymentFromAgent()
        {
            //Given or Arrange
            var phoneNumber = "+265888123321";
            var textMessage = @"
                Cash In from 263509-RODGERS LETALA on
                09/08/2020 09:43:32.
                Amt: 5,500.00MWK
                Fee: 0.00MWK
                Ref: 7H948UWUV8
                Bal: 5,557.96MWK
            ";

            //When or Act
            Payment payment = _service.GenerateFromMpamba(phoneNumber, textMessage);

            //Then or Assert
            payment.Amount.Should().Be(5500);
            payment.Reference.Should().Be("7H948UWUV8");
            payment.FromAgent.Should().BeTrue();
            payment.Amount.Should().BeOfType(typeof(Decimal));
        }
    }
}