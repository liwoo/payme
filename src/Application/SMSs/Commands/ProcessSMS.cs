using System.Threading;
using System.Threading.Tasks;
using Application.Common.Events;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Common.Validators;
using Application.SMSs.DTOs;
using Core.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using FluentValidation.Results;

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

        public async Task<string> Handle(ProcessSMS request, CancellationToken cancellationToken)
        {
            var sanitizedText = request.smsBody.Text.Replace("\"", string.Empty).Replace("\n", " ").Replace("\r", " ");
            var sanitizedPhone = request.smsBody.Phone.Replace(" ", string.Empty);

            var sms = new SMS()
            {
                Phone = sanitizedPhone,
                Message = sanitizedText
            };

            SMSValidator validator = new SMSValidator();
            ValidationResult result = validator.Validate(sms);

            if (result.IsValid)
            {
                var smsJson = JsonConvert.SerializeObject(sms);

                _logger.LogInformation(smsJson);

                _context.SMSs.Add(sms);

                await _context.SaveChangesAsync(cancellationToken);
                _logger.LogInformation($"Successfully saved SMS");


                return await _mediator.Send(new SMSReceived(new SMSContents
                {
                    Phone = sanitizedPhone,
                    Contents = sanitizedText
                }));
            }
            else
            {
                var message = "SMS is not valid: " + result.ToString();
                _logger.LogInformation(message);
                return message;
            }

        }
    }
}