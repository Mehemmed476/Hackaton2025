using AutoMapper;
using HotelManagementSystem.BL.DTOs.CustomerDTO;
using HotelManagementSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystem.BL.Profiles;

public class CustomerProfile : Profile
{
    public CustomerProfile()
    {
        CreateMap<AddCustomerDTO, Customer>().ReverseMap();
        CreateMap<GetCustomerDTO, Customer>().ReverseMap();
        CreateMap<UpdateCustomerDTO, Customer>().ReverseMap();
    }
}
