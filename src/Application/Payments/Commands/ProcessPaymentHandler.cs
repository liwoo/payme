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
                    //save!
                }
                
                return Task.FromCanceled(cancellationToken);

            }
            catch (UnprocessablePayment e)
            {
                //log this as an exception
                return Task.FromException(e);
            }
        }
    }
}