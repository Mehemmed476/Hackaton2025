using HotelManagementSystem.BL.DTOs.ServiceDTO;
using HotelManagementSystem.BL.Services.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagementSystem.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ServicesController : ControllerBase
{
    private readonly IServiceService _serviceService;

    public ServicesController(IServiceService serviceService)
    {
        _serviceService = serviceService;
    }

    [HttpGet("GetAllServices")]
    public async Task<IActionResult> GetAllServices()
    {
        try
        {
            return StatusCode(StatusCodes.Status200OK, await _serviceService.GetAllServices());
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status404NotFound, ex.Message);
        }
    }

    [HttpGet("GetServiceById/{id}")]
    public async Task<IActionResult> GetServiceById(Guid id)
    {
        try
        {
            return StatusCode(StatusCodes.Status200OK, await _serviceService.GetServiceById(id));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status404NotFound, ex.Message);
        }
    }

    [HttpPost("AddService")]
    public async Task<IActionResult> AddService(AddServiceDTO servicePostDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            await _serviceService.AddServiceAsync(servicePostDto);
            await _serviceService.SaveChangesAsync();
            return StatusCode(StatusCodes.Status201Created);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status404NotFound, ex.Message);
        }
    }

    [HttpPut("UpdateService")]
    public async Task<IActionResult> UpdateService(Guid id, UpdateServiceDTO servicePutDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            await _serviceService.Update(id, servicePutDto);
            await _serviceService.SaveChangesAsync();
            return StatusCode(StatusCodes.Status201Created);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status404NotFound, ex.Message);
        }
    }

    [HttpPut("SoftDeleteService/{id}")]
    public async Task<IActionResult> SoftDeleteService(Guid id)
    {
        try
        {
            await _serviceService.SoftDelete(id);
            await _serviceService.SaveChangesAsync();
            return StatusCode(StatusCodes.Status201Created);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status404NotFound, ex.Message);
        }
    }

    [HttpPut("RestoreService/{id}")]
    public async Task<IActionResult> RestoreService(Guid id)
    {
        try
        {
            await _serviceService.RevertSoftDelete(id);
            await _serviceService.SaveChangesAsync();
            return StatusCode(StatusCodes.Status201Created);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status404NotFound, ex.Message);
        }
    }

    [HttpDelete("DeleteService/{id}")]
    public async Task<IActionResult> DeleteService(Guid id)
    {
        try
        {
            await _serviceService.HardDelete(id);
            await _serviceService.SaveChangesAsync();
            return StatusCode(StatusCodes.Status201Created);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status404NotFound, ex.Message);
        }
    }
}
