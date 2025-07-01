using HotelManagementSystem.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystem.DL.Configurations;

public class ReservationConfigurations : IEntityTypeConfiguration<Reservation>
{
    public void Configure(EntityTypeBuilder<Reservation> builder)
    {
        builder.HasKey(r => r.Id);

        builder.Property(r => r.CheckInDate)
            .IsRequired();

        builder.Property(r => r.CheckOutDate)
            .IsRequired();


        builder.HasOne(r => r.Customer)
               .WithMany(c => c.Reservations)
               .HasForeignKey(r => r.CustomerId)
               .IsRequired()
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(r => r.Room)
               .WithMany(r => r.Reservations)
               .HasForeignKey(r => r.RoomId)
               .IsRequired()
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(r => new { r.CheckInDate, r.CheckOutDate });

        
        builder.ToTable(tb => tb.HasCheckConstraint(
            "CK_Reservation_CheckOutDate", 
            "[CheckOutDate] > [CheckInDate]"));
    }
}
