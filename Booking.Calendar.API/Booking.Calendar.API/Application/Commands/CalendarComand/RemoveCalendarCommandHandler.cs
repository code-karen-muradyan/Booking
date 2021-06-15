using Booking.Calendar.API.Infrastructure.Idempotency;
using Booking.Calendar.API.Infrastructure.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace Booking.Calendar.API.Application.Commands
{
    public class RemoveCalendarCommandHandler : IRequestHandler<RemoveCalendarCommand, bool>
    {
        private readonly IRepository _calendatRepository;
        public RemoveCalendarCommandHandler(IRepository calendatRepository)
        {
            _calendatRepository = calendatRepository;
        }
        public async Task<bool> Handle(RemoveCalendarCommand request, CancellationToken cancellationToken)
        {
            await _calendatRepository.RemoveCalendarAsync(request.Id);
            return true;

        }

    }


}
