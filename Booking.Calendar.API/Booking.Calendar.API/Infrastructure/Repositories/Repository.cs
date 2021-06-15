using Booking.Calendar.API.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Booking.Calendar.API.Infrastructure.Repositories
{
    public class Repository : IRepository
    {
        private readonly CalendarContext _calendarContext;
        public Repository(CalendarContext calendarContext)
        {
            _calendarContext = calendarContext;
        }
        public async Task<Apponintment> CreateAppointment(Apponintment apponintment)
        {
            _calendarContext.Apponintments.Add(apponintment);
            var result = await _calendarContext.SaveEntitiesAsync();
            if (result)
                return apponintment;
            else
                return null;
        }

        public async Task CreateCalendarAsync(Booking.Calendar.API.Models.Calendar calendar)
        {
            var k = _calendarContext.Calendars.Add(calendar);
            await _calendarContext.SaveChangesAsync();
        }

        public object GetAppointmentSpecificationByID(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveAppointment(int id)
        {
            var appointmentToDelate = _calendarContext.Apponintments.FirstOrDefault(x => x.Id == id);
            _calendarContext.Apponintments.Remove(appointmentToDelate);
            return _calendarContext.SaveEntitiesAsync();
        }

        public  Booking.Calendar.API.Models.Calendar GetCalendarById(int id)
        {
          var cal =  _calendarContext.Calendars.Find(new object[] { id });
            return cal;
        }

        public async Task RemoveCalendarAsync(int id)
        {
            var calendar = _calendarContext.Calendars.Find(new object[] { id });
             _calendarContext.Calendars.Remove(calendar);
            await _calendarContext.SaveChangesAsync();
        }

        public async Task<Apponintment> UpdateAppointment(Apponintment apponintment)
        {
            var app = _calendarContext.Apponintments.FirstOrDefault(x => x.Id == apponintment.Id);
           
            app.Title = apponintment.Title;
            app.Description = apponintment.Description;
            app.StartDate = apponintment.StartDate;
            app.EndDate = apponintment.EndDate;

            var res = await _calendarContext.SaveEntitiesAsync();
           
            if (res)
                return app;
            else return null;
        }
    }
}
