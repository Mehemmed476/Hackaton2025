using HotelManagementSystem.Core.Entities.Common;
using HotelManagementSystem.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystem.Core.Entities;

public class Room : AuditableEntity
{
    public string RoomNumber { get; set; }
    public RoomTypeEnum RoomType { get; set; }
    public decimal PricePerNight { get; set; }
    public RoomStatusEnum RoomStatus { get; set; }

    public ICollection<Reservation>? Reservations { get; set; } = new List<Reservation>();
}
