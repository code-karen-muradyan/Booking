using Booking.Calendar.API.Models.Write;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Booking.Calendar.API.Application.Validations
{
    public class CreateAppointmentCommandValidator : AbstractValidator<CreateAppointmentCommand>
    {
        public CreateAppointmentCommandValidator()
        {
            RuleFor(command => command.Title).NotEmpty();
        }

        private bool BeValidStartDate(DateTime dateTime)
        {
            return dateTime >= DateTime.UtcNow;
        }

      
    }
}
