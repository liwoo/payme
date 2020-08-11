using System;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Models;
using MediatR;

namespace Application.Common.Events
{
    public class SMSReceived : INotification
    {
        public readonly SMSContents sms;
        public SMSReceived(SMSContents sms)
        {
            this.sms = sms;
        }
    }

    public class SMSReceivedHandler : INotificationHandler<SMSReceived>
    {
        private readonly ILogger logger;
        public SMSReceivedHandler(ILogger<SMSReceivedHandler> logger)
        {
            this.logger = logger;
        }
        public Task Handle(SMSReceived notification, CancellationToken cancellationToken)
        {
            // TODO: Return Payment Object from MpambaService(sms)
            // try {
            // Payment payement = paymentService.GenerateFromMpamba(notification.sms)
            //                    || paymentService.GenerateFromAirtelMoney(notication.sms)
            // _context.savePayment(payment)
            // } catch {
            //  this.log.Information("SMS not Processable with either Mpamba or AM")
            //}
            // TODO: Return Payment Object from AirtelService(sms)
            return Task.CompletedTask;
        }
    }
}