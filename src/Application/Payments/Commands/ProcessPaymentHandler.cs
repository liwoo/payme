using System.Threading;
using System.Threading.Tasks;
using Application.Common.Events;
using Application.Common.Interfaces;
using Core.Exceptions;
using Core.Services;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Application.Payments.Commands
{
    public class ProcessPaymentHandler : IRequestHandler<SMSReceived, string>
    {
        private readonly ProviderService _providerService;
        private readonly IApplicationDbContext _context;
        private readonly ILogger _logger;

        public ProcessPaymentHandler(ILogger<ProcessPaymentHandler> logger, IApplicationDbContext context)
        {
            _providerService = new ProviderService();
            _context = context;
            _logger = logger;
        }

        public async Task<string> Handle(SMSReceived notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Saving stuff");
            try
            {
                Provider provider = _providerService.GetProviderName(notification.sms.Phone);

                if (provider == Provider.None)
                {
                    return "This provider does not exist"; //Task.FromCanceled(cancellationToken);
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
                    var paymentJson = JsonConvert.SerializeObject(payment);
                    _context.Payments.Add(payment);
                    await _context.SaveChangesAsync(cancellationToken);
                    _logger.LogInformation($"Payment Saved: {paymentJson}");

                    return "Payment Saved";
                }

                return "Not Deposit";
            }
            catch (UnprocessablePayment e)
            {
                _logger.LogError("Could not save payment", e);
                return "Could not save payment";// Task.FromException(e);
            }
        }
    }
}