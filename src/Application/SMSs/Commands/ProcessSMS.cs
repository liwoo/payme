using System.Threading;
using System.Threading.Tasks;
using Application.Common.Events;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.SMSs.DTOs;
using Core.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Application.SMSs.Commands
{

    public class ProcessSMS : IRequest<string>
    {
        public readonly SMSBodyDto smsBody;
        public ProcessSMS(SMSBodyDto smsBody)
        {
            this.smsBody = smsBody;
        }
    }
    public class ProcessSMSHandler : IRequestHandler<ProcessSMS, string>
    {
        private readonly IMediator _mediator;

        private readonly IApplicationDbContext _context;

        private readonly ILogger _logger;

        public ProcessSMSHandler(IMediator mediator, IApplicationDbContext context, ILogger<ProcessSMSHandler> logger)
        {
            _mediator = mediator;
            _context = context;
            _logger = logger;
        }

        public Task<string> Handle(ProcessSMS request, CancellationToken cancellationToken)
        {
            var sms = new SMS()
            {
                Phone = request.smsBody.Phone,
                Message = request.smsBody.Text.Replace("\"", string.Empty)
            };

            var smsJson = JsonConvert.SerializeObject(sms);

            _logger.LogInformation(smsJson);

            _context.SMSs.Add(sms);

            _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation($"Successfully saved SMS");


            _mediator.Publish(new SMSReceived(new SMSContents
            {
                Phone = request.smsBody.Phone,
                Contents = request.smsBody.Text
            }));

            return Task.FromResult("SMS Received Successfully");
        }
    }
}