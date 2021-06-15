using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Booking.Calendar.API.Application.Commands
{
    public class CreateCalendarCommand : IRequest<bool>
    {
        public string Name { get; set; }
    }

}
