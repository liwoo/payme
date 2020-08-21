using System.Threading;
using System.Threading.Tasks;
using Application.Common.Events;
using Core.Exceptions;
using Core.Services;
using MediatR;

namespace Application.Payments.Commands
{
    public class ProcessPaymentHandler : INotificationHandler<SMSReceived>
    {
        private readonly ProviderService _providerService;

        public ProcessPaymentHandler()
        {
            _providerService = new ProviderService();
        }

        public Task Handle(SMSReceived notification, CancellationToken cancellationToken)
        {
            try
            {
                Provider provider = _providerService.GetProviderName(notification.sms.Phone);

                if (provider == Provider.None)
                {
                    return Task.FromCanceled(cancellationToken);
                }

                IPaymentService service = _providerService.ServiceFromProviderFactory(provider, notification.sms.Phone, notification.sms.Contents);

                if (service.IsDeposit())
                {
                    var payment = service.GeneratePayment();
                    //check if ReferencePaymentExists
                    //if it doesn't exist - we good!
                    //if it exists
                    //Ask service if IsSamePayment(old, current)
                    //discard current one, i.e. do nothing
                    //email manager to reconcile manually
                    //save!
                }

                return Task.FromCanceled(cancellationToken);

            }
            catch (UnprocessablePayment e)
            {
                //log this as an exception
                //email manager
                return Task.FromException(e);
            }
        }
    }
}