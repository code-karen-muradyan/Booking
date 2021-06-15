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
    public class CreateCalendarCommandHandler : IRequestHandler<CreateCalendarCommand, bool>
    {
        private readonly IRepository _calendatRepository;
        public CreateCalendarCommandHandler(IRepository calendatRepository)
        {
            _calendatRepository = calendatRepository;
        }

        public async Task<bool> Handle(CreateCalendarCommand request, CancellationToken cancellationToken)
        {
            var result = false;
            var calendar = new Booking.Calendar.API.Models.Calendar
            {
                Name = request.Name
            };
             await _calendatRepository.CreateCalendarAsync(calendar);

            return result;
        }
    }

    public class CreateCalendarIdentifiedCommandHandler : IdentifiedCommandHandler<CreateCalendarCommand, bool>
    {
        public CreateCalendarIdentifiedCommandHandler(IMediator mediator, IRequestManager requestManager) : base(mediator, requestManager)
        {
        }

        protected override bool CreateResultForDuplicateRequest()
        {
            return true;                // Ignore duplicate requests for creating order.
        }
    }
}
