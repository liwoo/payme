using FluentValidation;
using Core.Entities;

namespace Application.Common.Validators
{
    public class SMSValidator: AbstractValidator<SMS>
    {
        public SMSValidator() {
            RuleFor( sms => sms.Phone).NotNull().Length(12);
            RuleFor( sms => sms.Message).NotNull().MinimumLength(3);
        }
    }
}