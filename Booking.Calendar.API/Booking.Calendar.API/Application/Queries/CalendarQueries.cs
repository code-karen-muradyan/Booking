using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Booking.Calendar.API.Models;
using Booking.Calendar.API.Models.Dto;
using Booking.Calendar.API.Models.ResponseModels;
using Dapper;

namespace Booking.Calendar.API.Application.Queries
{
    public class CalendarQueries : ICalendarQueries
    {
        private string _connectionString = string.Empty;

        public CalendarQueries(string constr)
        {
            _connectionString = !string.IsNullOrWhiteSpace(constr) ? constr : throw new ArgumentNullException(nameof(constr));
        }

        public async Task<AppointmentResponseModel> GetAppointmentAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var result = await connection.QueryFirstOrDefaultAsync<AppointmentResponseModel>(
                   @"select *
                        FROM Apponintments a                        
                        WHERE a.Id=@id"
                        , new { id }
                    );

                return result;
            }
        }

        public async Task<IEnumerable<AppointmentResponseModel>> GetAppointmentsAsync(string user, string categoria, DateTime date)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                return await connection.QueryAsync<AppointmentResponseModel>(@"select *
                        FROM apponintment a                        
                        WHERE a.To=@id AND a.Categoria=@categoria AND CAST(a.StartDate AS DATE) = CAST(@date AS DATE)"
                        , new { user, categoria, date }
                    );
            }
        }

        public async Task<IEnumerable<int>> GetCalendarIdsAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                return await connection.QueryAsync<int>(@"select ID FROM Calendar");
            }
        }


        public async Task<IEnumerable<AppointmentResponseModel>> GetAppointmentsByPeriodAsync(DateTime from, DateTime to, int calendarId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                return await connection.QueryAsync<AppointmentResponseModel>(@"select *
                        FROM Apponintments a                        
                        WHERE a.CalendarId=@calendarId  and  a.StartDate >= @from and a.EndDate <= @to"
                        , new { calendarId,from,to }
                    );
            }
        }


        public async Task<IEnumerable<AppointmentResponseModel>> GetAppointmentsAsync(int calendarId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                return await connection.QueryAsync<AppointmentResponseModel>(@"select *
                        FROM Apponintments a                        
                        WHERE a.CalendarId=@calendarId "
                        , new { calendarId}
                    );
            }
        }
    }
}
