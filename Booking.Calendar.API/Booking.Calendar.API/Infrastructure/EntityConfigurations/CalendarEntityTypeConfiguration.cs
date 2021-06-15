using Booking.Calendar.API.Models.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Booking.Calendar.API.Infrastructure.EntityConfigurations
{
    public class CalendarEntityTypeConfiguration:IEntityTypeConfiguration<Booking.Calendar.API.Models.Calendar>
    {
        public void Configure(EntityTypeBuilder<Booking.Calendar.API.Models.Calendar> calendarConfiguration)
        {
            calendarConfiguration.ToTable("Calendar");

            calendarConfiguration.HasKey(o => o.Id); 
            calendarConfiguration.Property(o => o.Id)
                .ForSqlServerUseSequenceHiLo("appointmentseq");
         
            calendarConfiguration.Property<string>("Name").IsRequired();   
         
          
        }
    }
}
