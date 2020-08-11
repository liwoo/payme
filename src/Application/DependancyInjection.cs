using System.ComponentModel.Design;
using Application.Common.Events;
using Application.SMS.Commands;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DependancyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(typeof(ProcessSMS));
            services.AddMediatR(typeof(SMSReceived));
            return services;
        }
    }
}