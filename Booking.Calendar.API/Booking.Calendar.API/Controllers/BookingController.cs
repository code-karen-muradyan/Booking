using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Booking.Calendar.API.Application.Commands;
using Booking.Calendar.API.Application.Queries;
using Booking.Calendar.API.Infrastructure.Repositories;
using Booking.Calendar.API.Models.Dto;
using Booking.Calendar.API.Models.ResponseModels;
using Booking.Calendar.API.Models.Write;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Booking.Calendar.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ICalendarQueries  _calendarQueries;

        public BookingController(IMediator mediator, ICalendarQueries calendarQueries)
        {

            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _calendarQueries = calendarQueries ?? throw new ArgumentNullException(nameof(calendarQueries));
        }


        /// <summary>
        /// This method returned all appointments for login user
        /// </summary>
        /// <returns></returns>
        [Route("items/calendar/{calendarId}")]
        [ProducesResponseType(typeof(List<AppointmentResponseModel>), (int)HttpStatusCode.OK)]
        [HttpGet]
        public async Task<IActionResult> GetBookedItems(int calendarId )
        {
            var apponintments =  await _calendarQueries.GetAppointmentsAsync(calendarId);
            return Ok(apponintments) ;
        }

        /// <summary>
        /// This method return appointment for id
        /// </summary>
        /// <param name="value"></param>
        [Route("items/{id}")]
        [ProducesResponseType(typeof(AppointmentResponseModel),(int)HttpStatusCode.OK)]
        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _calendarQueries.GetAppointmentAsync(id);
            return Ok(result);
        }


        [Route("items/from/{from}/to/{to}/calendarId/{calendarId}")]
        [ProducesResponseType(typeof(List<AppointmentResponseModel>), (int)HttpStatusCode.OK)]
        [HttpGet]
        public async Task<IActionResult> Get(DateTime from, DateTime to, int calendarId)
        {
            var result = await _calendarQueries.GetAppointmentsByPeriodAsync(from,  to, calendarId);
            return Ok(result);
        }

        /// <summary>
        /// This method update appointment data
        /// </summary>
        /// <param name="value"></param>
        [Route("items")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Post([FromBody] UpdateAppointmentCommand command, [FromHeader(Name = "x-requestid")] string requestId)
        {
            bool commandResult = false;
            if (Guid.TryParse(requestId, out Guid guid) && guid != Guid.Empty)
            {
                var requestCancelOrder = new IdentifiedCommand<UpdateAppointmentCommand, bool>(command, guid);
                commandResult = await _mediator.Send(requestCancelOrder);
            }
            return Ok(commandResult);
        }

        /// <summary>
        /// This method update data new appointment, local and for google calendar
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        [Route("items")]
        [HttpPut]
        [ProducesResponseType(typeof(Apponintment), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> Put([FromBody] CreateAppointmentCommand command, [FromHeader(Name = "x-requestid")] string requestId)
        {
            bool commandResult = false;
            if (Guid.TryParse(requestId, out Guid guid) && guid != Guid.Empty)
            {
                var requestCancelOrder = new IdentifiedCommand<CreateAppointmentCommand, bool>(command, guid);
                commandResult = await _mediator.Send(requestCancelOrder);
            }
            return Ok(commandResult);
        }


        [Route("items")]
        [HttpDelete]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete([FromQuery] RemoveAppointmentCommand command,[FromHeader(Name = "x-requestid")] string requestId)
        {
            bool commandResult = false;
            if (Guid.TryParse(requestId, out Guid guid) && guid != Guid.Empty)
            {
                var requestCancelOrder = new IdentifiedCommand<RemoveAppointmentCommand, bool>(command, guid);
                commandResult = await _mediator.Send(requestCancelOrder);
            }
            return Ok(commandResult);
        }



    }
}
