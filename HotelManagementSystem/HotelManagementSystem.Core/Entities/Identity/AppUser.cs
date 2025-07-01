using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystem.Core.Entities.Identity;

public class AppUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string PersonalImageURL { get; set; }
    public string CVImageURL { get; set; }
    public string IdentityCardImageURL { get; set; }
}
