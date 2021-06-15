using Booking.Calendar.API.Models.Dto;
using Booking.Calendar.API.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Booking.Calendar.API.Application.Queries
{
    public interface ICalendarQueries
    {
        Task<AppointmentResponseModel> GetAppointmentAsync(int id);

        Task<IEnumerable<AppointmentResponseModel>> GetAppointmentsAsync(string user, string categoria, DateTime date);
        Task<IEnumerable<int>> GetCalendarIdsAsync();
        Task<IEnumerable<AppointmentResponseModel>> GetAppointmentsAsync(int calendarId);
        Task<IEnumerable<AppointmentResponseModel>> GetAppointmentsByPeriodAsync(DateTime from, DateTime to, int calendarId);
    }
}
