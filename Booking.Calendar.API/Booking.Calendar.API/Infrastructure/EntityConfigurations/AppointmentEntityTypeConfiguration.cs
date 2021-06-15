using Booking.Calendar.API.Models.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Booking.Calendar.API.Infrastructure.EntityConfigurations
{
    public class AppointmentEntityTypeConfiguration : IEntityTypeConfiguration<Apponintment>
    {
        public void Configure(EntityTypeBuilder<Apponintment> builder)
        {
            builder.ToTable("Apponintment");

            builder
           .HasOne<Booking.Calendar.API.Models.Calendar>(s => s.Calendar)
           .WithMany(g => g.Apponintments)
           .HasForeignKey(s => s.CalendarId);

            builder.Property<DateTime>("StartDate").IsRequired();
            builder.Property<DateTime>("EndDate").IsRequired(false);
            builder.Property<string>("Title").IsRequired();
            builder.Property<string>("Description").IsRequired(false);



        }
    }
}
