using HotelManagementSystem.BL.DTOs.ReservationDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HotelManagementSystem.BL.Services.Abstractions;

namespace HotelManagementSystem.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReservationsController : ControllerBase
{
    private readonly IReservationService _reservationService;

    public ReservationsController(IReservationService reservationService)
    {
        _reservationService = reservationService;
    }

    [HttpGet("GetAllReservations")]
    public async Task<IActionResult> GetAllReservations()
    {
        try
        {
            return StatusCode(StatusCodes.Status200OK, await _reservationService.GetAllReservations());
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status404NotFound, ex.Message);
        }
    }

    [HttpGet("GetReservationById/{id}")]
    public async Task<IActionResult> GetReservationById(Guid id)
    {
        try
        {
            return StatusCode(StatusCodes.Status200OK, await _reservationService.GetReservationById(id));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status404NotFound, ex.Message);
        }
    }

    [HttpPost("AddReservation")]
    public async Task<IActionResult> AddReservation(AddReservationDTO reservationPostDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            await _reservationService.AddReservationAsync(reservationPostDto);
            await _reservationService.SaveChangesAsync();
            return StatusCode(StatusCodes.Status201Created);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status404NotFound, ex.Message);
        }
    }

    [HttpPut("UpdateReservation")]
    public async Task<IActionResult> UpdateReservation(Guid id, UpdateReservationDTO reservationPutDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            await _reservationService.Update(id, reservationPutDto);
            await _reservationService.SaveChangesAsync();
            return StatusCode(StatusCodes.Status201Created);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status404NotFound, ex.Message);
        }
    }

    [HttpPut("SoftDeleteReservation/{id}")]
    public async Task<IActionResult> SoftDeleteReservation(Guid id)
    {
        try
        {
            await _reservationService.SoftDelete(id);
            await _reservationService.SaveChangesAsync();
            return StatusCode(StatusCodes.Status201Created);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status404NotFound, ex.Message);
        }
    }

    [HttpPut("RestoreReservation/{id}")]
    public async Task<IActionResult> RestoreReservation(Guid id)
    {
        try
        {
            await _reservationService.RevertSoftDelete(id);
            await _reservationService.SaveChangesAsync();
            return StatusCode(StatusCodes.Status201Created);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status404NotFound, ex.Message);
        }
    }

    [HttpDelete("DeleteReservation/{id}")]
    public async Task<IActionResult> DeleteReservation(Guid id)
    {
        try
        {
            await _reservationService.HardDelete(id);
            await _reservationService.SaveChangesAsync();
            return StatusCode(StatusCodes.Status201Created);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status404NotFound, ex.Message);
        }
    }
}
