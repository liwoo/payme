using System.Threading;
using System.Threading.Tasks;
using Application.Common.Events;
using Application.Common.Models;
using Application.SMS.DTOs;
using MediatR;

namespace Application.SMS.Commands
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
        public ProcessSMSHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task<string> Handle(ProcessSMS request, CancellationToken cancellationToken)
        {
            // TODO: Save SMS to DB
            _mediator.Publish(new SMSReceived(new SMSContents
            {
                Phone = request.smsBody.Phone,
                Contents = request.smsBody.Text
            }
            ));

            // TODO: Return SavedSMS
            return Task.FromResult("SMS Saved");
        }
    }
}