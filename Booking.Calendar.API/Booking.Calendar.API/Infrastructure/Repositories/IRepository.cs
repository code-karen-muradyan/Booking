using Booking.Calendar.API.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Booking.Calendar.API.Infrastructure.Repositories
{
    public interface IRepository
    {
        /// <summary>
        /// Insert Appointment On DB
        /// </summary>
        /// <param name="apponintment">Appointment to Insert</param>
        /// <returns>Updated Appointment</returns>
        Task<Apponintment> CreateAppointment(Apponintment apponintment);
        /// <summary>
        /// Updates Appointment On DB
        /// </summary>
        /// <param name="apponintment">New Appointment</param>
        /// <returns>Updated Appointment</returns>
        Task<Apponintment> UpdateAppointment(Apponintment apponintment);
        Booking.Calendar.API.Models.Calendar GetCalendarById(int id);
        object GetAppointmentSpecificationByID(int id);

        /// <summary>
        /// remove Appointment From DB
        /// </summary>
        /// <param name="id">Appointment ID</param>
        /// <returns>Delated</returns>
        Task<bool> RemoveAppointment(int id);
        Task CreateCalendarAsync(Booking.Calendar.API.Models.Calendar calendar);
        Task RemoveCalendarAsync(int id);

    }
}
