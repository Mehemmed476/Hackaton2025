using AutoMapper;
using HotelManagementSystem.BL.DTOs.ServiceDTO;
using HotelManagementSystem.Core.Entities;

namespace HotelManagementSystem.BL.Profiles
{
    public class ServiceProfile : Profile
    {
        public ServiceProfile()
        {
            CreateMap<Service, AddServiceDTO>().ReverseMap();
            CreateMap<Service, GetServiceDTO>().ReverseMap();
            CreateMap<Service, UpdateServiceDTO>().ReverseMap();
        }
    }
}
