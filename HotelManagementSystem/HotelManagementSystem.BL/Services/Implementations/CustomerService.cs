using AutoMapper;
using HotelManagementSystem.BL.Customers.Abstractions;
using HotelManagementSystem.BL.DTOs.CustomerDTO;
using HotelManagementSystem.BL.Services.Abstractions;
using HotelManagementSystem.Core.Entities;
using HotelManagementSystem.Core.Entities.Identity;
using HotelManagementSystem.DL.Exceptions;
using HotelManagementSystem.DL.Repositories.Abstractions;
using System.Linq.Expressions;

namespace HotelManagementSystem.BL.Customers.Implementations;

public class CustomerService : ICustomerService
{
    readonly ICustomerReadRepository _readRepository;
    readonly ICustomerWriteRepository _writeRepository;
    readonly IAuthService _authService;
    readonly IMapper _mapper;

    public CustomerService(IAuthService authService, IMapper mapper, ICustomerReadRepository readRepository, ICustomerWriteRepository writeRepository)
    {
        _readRepository = readRepository;
        _writeRepository = writeRepository;
        _mapper = mapper;
        _authService = authService;
    }

    public async Task AddCustomerAsync(AddCustomerDTO addCustomerDTO)
    {
        Customer customer = _mapper.Map<Customer>(addCustomerDTO);

        AppUser user = await _authService.GetCurrentUserAsync();

        if (user is null)
        {
            throw new BaseException("Please login.");
        }

        customer.CreatedBy = user;
        customer.CreatedAt = DateTime.Now;
        await _writeRepository.CreateAsync(customer);
    }

    public async Task<ICollection<GetCustomerDTO>> GetAllCustomers()
    {
        return _mapper.Map<ICollection<GetCustomerDTO>>(await _readRepository.GetAllAsync());
    }

    public async Task<GetCustomerDTO> GetCustomerByCondition(Expression<Func<Customer, bool>> expression)
    {
        Customer customer = await _readRepository.GetOneByCondition(expression);

        return _mapper.Map<GetCustomerDTO>(customer);
    }

    public async Task<GetCustomerDTO> GetCustomerById(Guid Id)
    {
        Customer customer = await _readRepository.GetByIdAsync(Id);

        if (customer is null)
        {
            throw new BaseException("Couldn't find customer.");
        }

        return _mapper.Map<GetCustomerDTO>(customer);
    }

    public async Task HardDelete(Guid Id)
    {
        Customer customer = await _readRepository.GetByIdAsync(Id);

        if (customer is null)
        {
            throw new BaseException("Couldn't find customer.");
        }

        _writeRepository.Delete(customer);
    }

    public async Task RevertSoftDelete(Guid Id)
    {
        Customer customer = await _readRepository.GetByIdAsync(Id);

        AppUser user = await _authService.GetCurrentUserAsync();

        if (user is null)
        {
            throw new BaseException("Please login.");
        }
        if (customer is null)
        {
            throw new BaseException("Couldn't find customer.");
        }
        if (!customer.IsDeleted)
        {
            throw new BaseException("Customer is already reverted.");
        }

        customer.IsDeleted = false;
        customer.DeletedBy = null;
        customer.DeletedAt = null;

        _writeRepository.Update(customer);
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _writeRepository.SaveChangesAsync();
    }

    public async Task SoftDelete(Guid Id)
    {
        Customer customer = await _readRepository.GetByIdAsync(Id);

        AppUser user = await _authService.GetCurrentUserAsync();

        if (user is null)
        {
            throw new BaseException("Please login.");
        }
        if (customer is null)
        {
            throw new BaseException("Couldn't find customer.");
        }
        if (customer.IsDeleted)
        {
            throw new BaseException("Customer is already deleted.");
        }

        customer.IsDeleted = true;
        customer.DeletedBy = user;
        customer.DeletedAt = DateTime.Now;

        _writeRepository.Update(customer);
    }

    public async Task Update(Guid Id, UpdateCustomerDTO updateCustomerDTO)
    {
        Customer customer = await _readRepository.GetByIdAsync(Id);

        AppUser user = await _authService.GetCurrentUserAsync();

        if (user is null)
        {
            throw new BaseException("Please login.");
        }
        if (customer.IsDeleted)
        {
            throw new BaseException("Customer is deleted. Please revert it before updating it.");
        }
        if (customer is null)
        {
            throw new BaseException("Couldn't find customer.");
        }
        if (customer.IsDeleted)
        {
            throw new BaseException("Customer is already deleted.");
        }

        Customer updatedCustomer = _mapper.Map<Customer>(updateCustomerDTO);

        updatedCustomer.Id = Id;
        updatedCustomer.CreatedBy = customer.CreatedBy;
        updatedCustomer.CreatedAt = customer.CreatedAt;
        updatedCustomer.UpdatedAt = DateTime.Now;
        updatedCustomer.UpdatedBy = user;

        _writeRepository.Update(updatedCustomer);
    }
}
