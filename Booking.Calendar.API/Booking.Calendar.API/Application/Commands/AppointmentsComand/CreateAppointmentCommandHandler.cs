using Booking.Calendar.API.Infrastructure.Idempotency;
using Booking.Calendar.API.Infrastructure.Repositories;
using Booking.Calendar.API.Models.Write;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Booking.Calendar.API.Models.Dto;
using System.Threading.Tasks;

namespace Booking.Calendar.API.Application.Commands
{
    public class CreateAppointmentCommandHandler : IRequestHandler<CreateAppointmentCommand, bool>
    {
        private readonly IRepository _calendatRepository;
        public CreateAppointmentCommandHandler(IRepository calendatRepository)
        {
            _calendatRepository = calendatRepository;
        }

        public async Task<bool> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
        {
            var result = false;
            var apponintment = new Models.Dto.Apponintment
            {
                Description = request.Description,
                Title = request.Title,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                CalendarId = request.CalendarId
            };
            var res = await _calendatRepository.CreateAppointment(apponintment);
            if (res != null)
            {
               
            }
            return result;
        }
    }

    public class CreateAppointmentIdentifiedCommandHandler : IdentifiedCommandHandler<CreateAppointmentCommand, bool>
    {
        public CreateAppointmentIdentifiedCommandHandler(IMediator mediator, IRequestManager requestManager) : base(mediator, requestManager)
        {
        }

        protected override bool CreateResultForDuplicateRequest()
        {
            return true;                //TODO Ignore duplicate requests for creating appointment 
        }
    }
}
