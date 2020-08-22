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

            var sanitizedMessage = @"CashInfrom263509-RODGERSLETALAon09/08/202009:43:32.Amt:5,500.00MWKFee:0.00MWKRef:7H948UWUV8Bal:5,557.96MWK";

            //When or Act
            MpambaService mpambaService = GetService(phoneNumber, textMessage);
            mpambaService._message.Should().Be(sanitizedMessage);
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
            payment.BankName.Should().Be(Bank.AirtelMoney);
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
        [Fact]
        public void PaymentService_ShouldGenerateAirtelMoneyPaymentFromAgent()
        {
            var phoneNumber = "+265999123321";
            var textMessage = @"Txn Id:ER200605.1800.H19376. Dear Customer, you have recieved MK 68750.00 from  . Your available balance is  MK68852.69.";

            AirtelMoneyService service = new AirtelMoneyService(textMessage, phoneNumber);
            Payment payment = service.GeneratePayment();

            payment.Amount.Should().Be(68750);
            payment.Reference.Should().Be("ER200605.1800.H19376");
            payment.FromAgent.Should().Be(true);
        }
        [Fact]
        public void PaymentService_ShouldGenerateAirtelMoneyPaymentFromUser()
        {
            var phoneNumber = "+265999123321";
            var textMessage = @"Trans.ID :  PP200602.1133.H23975. Dear customer, you have received MK 3000.00 from 990000000,FIRSTNAME LASTNAME . Your available balance is MK 4022.69.";

            AirtelMoneyService service = new AirtelMoneyService(textMessage, phoneNumber);
            Payment payment = service.GeneratePayment();

            payment.Amount.Should().Be(3000);
            payment.Reference.Should().Be("PP200602.1133.H23975");
        }
        [Fact]
        public void PaymentService_ShouldGenerateAirtelMoneyPaymentFromBank()
        {
            var phoneNumber = "+265888123321";
            var textMessage = @"Trans. ID: BW200602.1151.D34302 You have received MK 5000.00 from Bank Account. Your available balance is 9022.69MK.";

            AirtelMoneyService service = new AirtelMoneyService(textMessage, phoneNumber);
            Payment payment = service.GeneratePayment();

            payment.Amount.Should().Be(5000);
            payment.Reference.Should().Be("BW200602.1151.D34302");
            payment.BankName.Should().Be(Bank.Missing);
        }

        [Fact]
        public void PaymentService_ShouldBeValidTransactionAirtel()
        {
            var phoneNumber = "+265888123321";
            var textMessage = @"Trans. ID: BW200602.1151.D34302 You have received MK 5000.00 from Bank Account. Your available balance is 9022.69MK.";

            AirtelMoneyService service = new AirtelMoneyService(textMessage, phoneNumber);

            service.HasInvalidReference().Should().Be(true);
            service.IsDeposit().Should().Be(true);
        }

        [Fact]
        public void PaymentService_ShouldBeValidTransactionMpamba()
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

            MpambaService service = GetService(phoneNumber, textMessage);

            service.HasInvalidReference().Should().Be(true);
            service.IsDeposit().Should().Be(true);
        }
    }
}