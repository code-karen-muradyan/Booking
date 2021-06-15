using Booking.Calendar.API.Infrastructure.Idempotency;
using Booking.Calendar.API.Infrastructure.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Booking.Calendar.API.Models.Dto;
using System.Threading.Tasks;

namespace Booking.Calendar.API.Application.Commands
{
    public class UpdateAppointmentCommandHandler : IRequestHandler<UpdateAppointmentCommand, bool>
    {
        private readonly IRepository _repository;
        public UpdateAppointmentCommandHandler(IRepository calendarRepository)
        {
            _repository = calendarRepository;
        }
        public async Task<bool> Handle(UpdateAppointmentCommand request, CancellationToken cancellationToken)
        {
            var result = false;

            Apponintment ap = new Apponintment
            {
                Description = request.Description,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                Title = request.Title,
                Id = request.Id
            };

            Apponintment res  = await  _repository.UpdateAppointment(ap);
            
            if (res != null)
            {
                result = true;
            }  
            return result;
        }

        public class UpdateAppointmentIdentifiedCommandHandler : IdentifiedCommandHandler<UpdateAppointmentCommand, bool>
        {
            public UpdateAppointmentIdentifiedCommandHandler(IMediator mediator, IRequestManager requestManager) : base(mediator, requestManager)
            {
            }

            protected override bool CreateResultForDuplicateRequest()
            {
                return true;                // Ignore duplicate requests for creating order.
            }
        }
    }
}
