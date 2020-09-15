using System;
using Core.Entities;
using Core.Services;
using FluentAssertions;
using Xunit;

namespace Domain.UnitTests.Services
{
    public class AirtelMoneyPaymentServiceTest
    {

        private AirtelMoneyService GetService(string phoneNumber, string textMessage)
            => new AirtelMoneyService(textMessage, phoneNumber);

        [Fact]
        public void PaymentService_ShouldWork()
        {
            true.Should().BeTrue();
        }


        [Fact]
        public void PaymentService_ShouldGenerateAirtelMoneyPaymentFromAgent()
        {
            var phoneNumber = "+265999123321";
            var textMessage = "Txn Id:ER200605.1800.H19376. Dear Customer, you have recieved MK 68750.00 from  . Your available balance is  MK68852.69.";

            AirtelMoneyService service = GetService(phoneNumber, textMessage);
            Payment payment = service.GeneratePayment();

            payment.Amount.Should().Be(68750);
            payment.Reference.Should().Be("ER200605.1800.H19376");
            payment.AgentName.Should().Be("Missing");
        }
        [Fact]
        public void PaymentService_ShouldGenerateAirtelMoneyPaymentFromUser()
        {
            var phoneNumber = "+265999123321";
            var textMessage = "Trans.ID :  PP200602.1133.H23975. Dear customer, you have received MK 3000.00 from 990000000,FIRSTNAME LASTNAME . Your available balance is MK 4022.69.";

            AirtelMoneyService service = GetService(phoneNumber, textMessage);
            Payment payment = service.GeneratePayment();

            payment.Amount.Should().Be(3000);
            payment.Reference.Should().Be("PP200602.1133.H23975");
            payment.SenderName.Should().Be("FIRSTNAMELASTNAME");
        }
        [Fact]
        public void PaymentService_ShouldGenerateAirtelMoneyPaymentFromBank()
        {
            var phoneNumber = "+265888123321";
            var textMessage = "Trans. ID: BW200602.1151.D34302 You have received MK 5000.00 from Bank Account. Your available balance is 9022.69MK.";

            AirtelMoneyService service = GetService(phoneNumber, textMessage);
            Payment payment = service.GeneratePayment();

            payment.Amount.Should().Be(5000);
            payment.Reference.Should().Be("BW200602.1151.D34302");
            payment.BankName.Should().Be(Bank.Missing);
        }

        [Fact]
        public void PaymentService_ShouldBeValidAirtelTransaction()
        {
            var phoneNumber = "+265888123321";
            var textMessage = "Trans. ID: BW200602.1151.D34302 You have received MK 5000.00 from Bank Account. Your available balance is 9022.69MK.";

            AirtelMoneyService service = GetService(phoneNumber, textMessage);

            service.HasInvalidReference().Should().Be(false);
            service.IsDeposit().Should().Be(true);
        }
    }
}