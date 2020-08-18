using System.Threading;
using System.Threading.Tasks;
using Application.Common.Events;
using MediatR;

namespace Application.Payments.Commands
{
    public class ProcessPaymentHandler : INotificationHandler<SMSReceived>
    {
        public Task Handle(SMSReceived notification, CancellationToken cancellationToken)
        {
            //if Service Provider is TNM or Airtel then we have a potential Paymnent
                // PaymentService.GenerateFromMpamba or AM
                // catch an Error Here then Mail our Manager
                // no error -> Persist our Payment Object into our Database (EFCore) .save()
            throw new System.NotImplementedException();
        }
    }
}