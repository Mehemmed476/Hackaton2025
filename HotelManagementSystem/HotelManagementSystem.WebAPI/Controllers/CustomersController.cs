﻿using HotelManagementSystem.BL.Customers.Abstractions;
using HotelManagementSystem.BL.DTOs.CustomerDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagementSystem.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomersController : ControllerBase
{
    private readonly ICustomerService _customerService;

    public CustomersController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    [HttpGet("GetAllCustomers")]
    public async Task<IActionResult> GetAllCustomers()
    {
        try
        {
            return StatusCode(StatusCodes.Status200OK, await _customerService.GetAllCustomers());
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status404NotFound, ex.Message);
        }
    }

    [HttpGet("GetCustomerById/{id}")]
    public async Task<IActionResult> GetCustomerById(Guid id)
    {
        try
        {
            return StatusCode(StatusCodes.Status200OK, await _customerService.GetCustomerById(id));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status404NotFound, ex.Message);
        }
    }

    [HttpPost("AddCustomer")]
    public async Task<IActionResult> AddCustomer(AddCustomerDTO customerPostDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            await _customerService.AddCustomerAsync(customerPostDto);
            await _customerService.SaveChangesAsync();
            return StatusCode(StatusCodes.Status201Created);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status404NotFound, ex.Message);
        }
    }

    [HttpPut("UpdateCustomer")]
    public async Task<IActionResult> UpdateCustomer(Guid id, UpdateCustomerDTO customerPutDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            await _customerService.Update(id, customerPutDto);
            await _customerService.SaveChangesAsync();
            return StatusCode(StatusCodes.Status201Created);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status404NotFound, ex.Message);
        }
    }

    [HttpPut("SoftDeleteCustomer/{id}")]
    public async Task<IActionResult> SoftDeleteCustomer(Guid id)
    {
        try
        {
            await _customerService.SoftDelete(id);
            await _customerService.SaveChangesAsync();
            return StatusCode(StatusCodes.Status201Created);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status404NotFound, ex.Message);
        }
    }

    [HttpPut("RestoreCustomer/{id}")]
    public async Task<IActionResult> RestoreCustomer(Guid id)
    {
        try
        {
            await _customerService.RevertSoftDelete(id);
            await _customerService.SaveChangesAsync();
            return StatusCode(StatusCodes.Status201Created);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status404NotFound, ex.Message);
        }
    }

    [HttpDelete("DeleteCustomer/{id}")]
    public async Task<IActionResult> DeleteCustomer(Guid id)
    {
        try
        {
            await _customerService.HardDelete(id);
            await _customerService.SaveChangesAsync();
            return StatusCode(StatusCodes.Status201Created);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status404NotFound, ex.Message);
        }
    }

}
