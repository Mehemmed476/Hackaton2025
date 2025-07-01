using AutoMapper;
using HotelManagementSystem.BL.DTOs.ReservationDTO;
using HotelManagementSystem.BL.DTOs.RoomDTO;
using HotelManagementSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystem.BL.Profiles;

class ReservationProfile : Profile
{
    public ReservationProfile()
    {
        CreateMap<AddReservationDTO, Reservation>().ReverseMap();
        CreateMap<GetReservationDTO, Reservation>().ReverseMap();
        CreateMap<UpdateReservationDTO, Reservation>().ReverseMap();
    }
}
