using AutoMapper;
using HotelManagementSystem.BL.DTOs.Room;
using HotelManagementSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystem.BL.Profiles;

public class RoomProfile : Profile
{
    public RoomProfile()
    {
        CreateMap<AddRoomDTO, Room>().ReverseMap();
        CreateMap<GetRoomDTO, Room>().ReverseMap();
        CreateMap<UpdateRoomDTO, Room>().ReverseMap();
    }
}
