using Booking.Calendar.API.Application.Commands;
using Booking.Calendar.API.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Booking.Calendar.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CalendarController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ICalendarQueries _calendarQueries;

        public CalendarController(IMediator mediator, ICalendarQueries calendarQueries)
        {

            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _calendarQueries = calendarQueries ?? throw new ArgumentNullException(nameof(calendarQueries));
        }


        /// <summary>
        /// This method returned all appointments for login user
        /// </summary>
        /// <returns></returns>
        [Route("items")]
        [ProducesResponseType(typeof(IEnumerable<int>), (int)HttpStatusCode.OK)]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _calendarQueries.GetCalendarIdsAsync();
            return Ok(result);
        }


        /// <summary>
        /// This method update data new appointment, local and for  calendar
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        [Route("items")]
        [HttpPut]
        [ProducesResponseType(typeof(Booking.Calendar.API.Models.Calendar), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> Put([FromBody] CreateCalendarCommand command, [FromHeader(Name = "x-requestid")] string requestId)
        {
            bool commandResult = false;
            if (Guid.TryParse(requestId, out Guid guid) && guid != Guid.Empty)
            {
                var requestCancelOrder = new IdentifiedCommand<CreateCalendarCommand, bool>(command, guid);
                commandResult = await _mediator.Send(requestCancelOrder);
            }
            return Ok(commandResult);
        }

        [Route("items")]
        [HttpDelete]
        [ProducesResponseType(typeof(Booking.Calendar.API.Models.Calendar), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> Remove([FromBody] RemoveCalendarCommand command)
        {
            bool commandResult = await _mediator.Send(command);
            return Ok(commandResult);
        }



    }
















}
