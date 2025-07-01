using HotelManagementSystem.Core.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystem.Core.Entities;

public class Reservation : AuditableEntity
{
    public Guid CustomerId { get; set; }
    public Guid RoomId { get; set; }
    public DateTime CheckInDate { get; set; }
    public DateTime CheckOutDate { get; set; }

    
    public Room? Room { get; set; }
    public Customer? Customer { get; set; }
    public ICollection<Service> Services { get; set; } = new List<Service>();
}
