using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Booking.Calendar.API.Application.Commands
{
    public class UpdateAppointmentCommand : IRequest<bool>
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Id { get; set; }
    }
}
