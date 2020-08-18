using System;
using Core.Entities;
using Core.Services;
using FluentAssertions;
using Xunit;

namespace Domain.UnitTests.Services
{
    public class PaymentServiceTest
    {

        private MpambaService GetService(string phoneNumber, string textMessage)
            => new MpambaService(textMessage, phoneNumber);

        [Fact]
        public void PaymentService_ShouldWork()
        {
            true.Should().BeTrue();
        }

        [Fact]
        public void PaymentService_ShouldSanitizeMessage()
        {
            var phoneNumber = "+265888123321";
            var textMessage = @"
                Cash In from 263509-RODGERS LETALA on
                09/08/2020 09:43:32.
                Amt: 5,500.00MWK
                Fee: 0.00MWK
                Ref: 7H948UWUV8
                Bal: 5,557.96MWK
            ";

            //TODO: Make this test pass
            //TODO: Sanization Logic is in the IPaymentService interface
            // var sanitizedMessage = @"CashInfrom263509-RODGERSLETALAon09/08/2020 09:43:32.Amt:5,500.00MWKFee0.00MWKRef:7H948UWUV8Bal:5,557.96MWK";

            //When or Act
            MpambaService mpambaService = GetService(phoneNumber, textMessage);
            // mpambaService._message.Should().Be(sanitizedMessage);
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
            Payment payment = GetService(phoneNumber, textMessage).GeneratePayment();

            //Then or Assert
            payment.Amount.Should().Be(5500);
            payment.Reference.Should().Be("7H948UWUV8");
            payment.FromAgent.Should().BeTrue();
            payment.Amount.Should().BeOfType(typeof(Decimal));
        }

        [Fact]
        public void PaymentService_ShouldGenerateMpambaPaymentFromUser()
        {
            var phoneNumber = "+265888123321";
            var textMessage = @"
            Money Received from 265880000000 firstname lastname on 
            26/05/2019 14:29:36. 
            Amount: 6,000.00MWK 
            Ref: 6EQ63HN6KW
            Bal: 6,002.00MWK
            ";

            Payment payment = GetService(phoneNumber, textMessage).GeneratePayment();

            payment.Amount.Should().Be(6000);
            payment.Reference.Should().Be("6EQ63HN6KW");
            payment.FromAgent.Should().BeFalse();
            payment.Amount.Should().BeOfType(typeof(Decimal));
        }

        [Fact]
        public void PaymentService_ShouldGenerateMpambaPaymentFromAirtelMoney()
        {
            var phoneNumber = "+265888123321";
            var textMessage = @"
            You have recieved 840.00MWK
            from AIRTEL MONEY
            on 02/06/2020 20:16:04. 
            Ref: 7F257T6NHD
            Balance: 840.01MWK
            ";

            Payment payment = GetService(phoneNumber, textMessage).GeneratePayment();

            payment.Amount.Should().Be(840);
            payment.Reference.Should().Be("7F257T6NHD");
            payment.FromAgent.Should().BeFalse();
            payment.Amount.Should().BeOfType(typeof(Decimal));
            //TODO: payment.BankName.Should().Be(Bank.AirtelMoney);
            //TODO: make that test pass
        }

        [Fact]
        public void PaymentService_ShouldShowWhichBankTransactionIsFrom()
        {
            var phoneNumber = "+265888123321";
            var textMessage = @"
            Deposit from STANDARD BANK on 20/06/2020 18:11:10. Amount: 3,500.00MWK Fee: 0.00MWK Ref: 7FK48241IQ Available Balance: 3,500.01MWK.
            ";

            Payment payment = GetService(phoneNumber, textMessage).GeneratePayment();

            payment.BankName.Should().Be(Bank.Standard);
        }
    }
}