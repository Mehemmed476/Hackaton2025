using HotelManagementSystem.Core.Entities.Identity;

namespace HotelManagementSystem.Core.Entities.Common;

public abstract class AuditableEntity : BaseEntity
{
    public DateTime CreatedAt { get; set; }
    public AppUser CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public AppUser? UpdatedBy { get; set; }
    public DateTime? DeletedAt { get; set; }
    public AppUser? DeletedBy { get; set; }
}
