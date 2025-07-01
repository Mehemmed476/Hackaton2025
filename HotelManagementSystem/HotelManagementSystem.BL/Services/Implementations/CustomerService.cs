using AutoMapper;
using HotelManagementSystem.BL.DTOs.CustomerDTO;
using HotelManagementSystem.Core.Entities;
using HotelManagementSystem.DL.Exceptions;
using HotelManagementSystem.DL.Repositories.Abstractions;
using System.Linq.Expressions;

namespace HotelManagementSystem.BL.Customers.Implementations;

public class CustomerService
{
    readonly ICustomerReadRepository _readRepository;
    readonly ICustomerWriteRepository _writeRepository;
    readonly IMapper _mapper;

    public CustomerService(IMapper mapper, ICustomerReadRepository readRepository, ICustomerWriteRepository writeRepository)
    {
        _readRepository = readRepository;
        _writeRepository = writeRepository;
        _mapper = mapper;
    }

    public async Task AddCustomerAsync(AddCustomerDTO addCustomerDTO)
    {
        Customer customer = _mapper.Map<Customer>(addCustomerDTO);
        await _writeRepository.CreateAsync(customer);
        //WARNING ADD ADDED BY

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

        if (customer is null)
        {
            throw new BaseException("Couldn't find customer.");
        }
        if (!customer.IsDeleted)
        {
            throw new BaseException("Customer is already reverted.");
        }

        customer.IsDeleted = false;

        //WARNING ADD DELETED BY

        _writeRepository.Update(customer);
    }

    public Task<int> SaveChangesAsync()
    {
        throw new NotImplementedException();
    }

    public async Task SoftDelete(Guid Id)
    {
        Customer customer = await _readRepository.GetByIdAsync(Id);

        if (customer is null)
        {
            throw new BaseException("Couldn't find customer.");
        }
        if (customer.IsDeleted)
        {
            throw new BaseException("Customer is already deleted.");
        }

        customer.IsDeleted = true;

        //WARNING ADD DELETED BY

        _writeRepository.Update(customer);
    }

    public async Task Update(Guid Id, UpdateCustomerDTO updateCustomerDTO)
    {
        Customer customer = await _readRepository.GetByIdAsync(Id);

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

        //WARNING ADD DELETED BY

        _writeRepository.Update(updatedCustomer);
    }
}
