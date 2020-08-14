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
            //Call our Payment Service to generate a Payment Object
            //Persist our Payment Object into our Database (EFCore) .save()
            throw new System.NotImplementedException();
        }
    }
}