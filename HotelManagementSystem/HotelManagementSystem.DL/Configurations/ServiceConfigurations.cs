using HotelManagementSystem.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystem.DL.Configurations;

public class ServiceConfigurations : IEntityTypeConfiguration<Service>
{
    public void Configure(EntityTypeBuilder<Service> builder)
    {
        builder.HasKey(s => s.Id);


        builder.Property(s => s.Title)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(s => s.Price)
            .IsRequired()
            .HasColumnType("decimal(18,2)");


        builder.HasOne(s => s.Reservation) 
               .WithMany(r => r.Services) 
               .HasForeignKey(s => s.ReservationId) 
               .OnDelete(DeleteBehavior.Cascade);
    }
}
