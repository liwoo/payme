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
}