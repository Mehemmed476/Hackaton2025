using AutoMapper;
using HotelManagementSystem.BL.DTOs.ServiceDTO;
using HotelManagementSystem.BL.Services.Abstractions;
using HotelManagementSystem.Core.Entities;
using HotelManagementSystem.Core.Entities.Identity;
using HotelManagementSystem.DL.Exceptions;
using HotelManagementSystem.DL.Repositories.Abstractions;
using Microsoft.AspNetCore.Identity;
using System.Linq.Expressions;

namespace HotelManagementSystem.BL.Services.Implementations;

public class ServiceService : IServiceService
{
    readonly IServiceReadRepository _readRepository;
    readonly IServiceWriteRepository _writeRepository;
    readonly IMapper _mapper;
    readonly IAuthService _authService;

    public ServiceService(IAuthService authService, IMapper mapper, IServiceReadRepository readRepository, IServiceWriteRepository writeRepository)
    {
        _readRepository = readRepository;
        _writeRepository = writeRepository;
        _mapper = mapper;
        _authService = authService;
    }

    public async Task AddServiceAsync(AddServiceDTO addServiceDTO)
    {
        Service service = _mapper.Map<Service>(addServiceDTO);

        //AppUser user = await _authService.GetCurrentUserAsync();

        //if (user is null)
        //{
        //    throw new BaseException("Please login.");
        //}

        //service.CreatedBy = user;
        service.CreatedAt = DateTime.Now;

        await _writeRepository.CreateAsync(service);
    }

    public async Task<ICollection<GetServiceDTO>> GetAllServices()
    {
        return _mapper.Map<ICollection<GetServiceDTO>>(await _readRepository.GetAllAsync());
    }

    public async Task<GetServiceDTO> GetServiceByCondition(Expression<Func<Service, bool>> expression)
    {
        Service service = await _readRepository.GetOneByCondition(expression);

        return _mapper.Map<GetServiceDTO>(service);
    }

    public async Task<GetServiceDTO> GetServiceById(Guid Id)
    {
        Service service = await _readRepository.GetByIdAsync(Id, false, "Reservation");


        if (service is null)
        {
            throw new BaseException("Couldn't find service.");
        }

        return _mapper.Map<GetServiceDTO>(service);
    }

    public async Task HardDelete(Guid Id)
    {
        Service service = await _readRepository.GetByIdAsync(Id);

        if (service is null)
        {
            throw new BaseException("Couldn't find service.");
        }

        _writeRepository.Delete(service);
    }

    public async Task RevertSoftDelete(Guid Id)
    {
        Service service = await _readRepository.GetByIdAsync(Id);

        //AppUser user = await _authService.GetCurrentUserAsync();

        //if (user is null)
        //{
        //    throw new BaseException("Please login.");
        //}
        if (service is null)
        {
            throw new BaseException("Couldn't find service.");
        }
        if (!service.IsDeleted)
        {
            throw new BaseException("Service is already reverted.");
        }

        service.IsDeleted = false;
        service.DeletedBy = null;
        service.DeletedAt = null;

        _writeRepository.Update(service);
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _writeRepository.SaveChangesAsync();
    }

    public async Task SoftDelete(Guid Id)
    {
        Service service = await _readRepository.GetByIdAsync(Id);

        //AppUser user = await _authService.GetCurrentUserAsync();

        //if (user is null)
        //{
        //    throw new BaseException("Please login.");
        //}
        if (service is null)
        {
            throw new BaseException("Couldn't find service.");
        }
        if (service.IsDeleted)
        {
            throw new BaseException("Service is already deleted.");
        }

        service.IsDeleted = true;
        //service.DeletedBy = user;
        service.DeletedAt = DateTime.Now;

        _writeRepository.Update(service);
    }

    public async Task Update(Guid Id, UpdateServiceDTO updateServiceDTO)
    {
        Service service = await _readRepository.GetByIdAsync(Id);

        //AppUser user = await _authService.GetCurrentUserAsync();

        //if (user is null)
        //{
        //    throw new BaseException("Please login.");
        //}
        if (service is null)
        {
            throw new BaseException("Couldn't find service.");
        }
        if (service.IsDeleted)
        {
            throw new BaseException("Service is already deleted.");
        }

        Service updatedService = _mapper.Map<Service>(updateServiceDTO);

        updatedService.Id = Id;
        //updatedService.CreatedBy = service.CreatedBy;
        updatedService.CreatedAt = service.CreatedAt;
        updatedService.UpdatedAt = DateTime.Now;
        //updatedService.UpdatedBy = user;

        _writeRepository.Update(updatedService);
    }
}
