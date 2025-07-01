using HotelManagementSystem.Core.Entities;
using HotelManagementSystem.Core.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystem.DL.Configurations;

public class RoomConfigurations : IEntityTypeConfiguration<Room>
{
    public void Configure(EntityTypeBuilder<Room> builder)
    {
        builder.HasKey(r => r.Id);

        builder.Property(r => r.RoomNumber)
            .IsRequired()
            .HasMaxLength(10); 

        builder.Property(r => r.PricePerNight)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(r => r.RoomType)
            .IsRequired()
            .HasConversion<string>();

        builder.Property(r => r.RoomStatus)
            .IsRequired()
            .HasConversion<string>()
            .HasDefaultValue(RoomStatusEnum.Available);


        builder.HasIndex(r => r.RoomNumber)
            .IsUnique();

        builder.HasIndex(r => new { r.RoomType, r.RoomStatus });
    }
}
