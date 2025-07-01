using HotelManagementSystem.BL.DTOs.RoomDTO;
using HotelManagementSystem.BL.Services.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagementSystem.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RoomsController : ControllerBase
{
    private readonly IRoomService _roomService;

    public RoomsController(IRoomService roomService)
    {
        _roomService = roomService;
    }

    [HttpGet("GetAllRooms")]
    public async Task<IActionResult> GetAllRooms()
    {
        try
        {
            return StatusCode(StatusCodes.Status200OK, await _roomService.GetAllRooms());
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status404NotFound, ex.Message);
        }
    }

    [HttpGet("GetRoomById/{id}")]
    public async Task<IActionResult> GetRoomById(Guid id)
    {
        try
        {
            return StatusCode(StatusCodes.Status200OK, await _roomService.GetRoomById(id));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status404NotFound, ex.Message);
        }
    }

    [HttpPost("AddRoom")]
    public async Task<IActionResult> AddRoom(AddRoomDTO roomPostDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            await _roomService.AddRoomAsync(roomPostDto);
            await _roomService.SaveChangesAsync();
            return StatusCode(StatusCodes.Status201Created);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status404NotFound, ex.Message);
        }
    }

    [HttpPut("UpdateRoom")]
    public async Task<IActionResult> UpdateRoom(Guid id, UpdateRoomDTO roomPutDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            await _roomService.Update(id, roomPutDto);
            await _roomService.SaveChangesAsync();
            return StatusCode(StatusCodes.Status201Created);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status404NotFound, ex.Message);
        }
    }

    [HttpPut("SoftDeleteRoom/{id}")]
    public async Task<IActionResult> SoftDeleteRoom(Guid id)
    {
        try
        {
            await _roomService.SoftDelete(id);
            await _roomService.SaveChangesAsync();
            return StatusCode(StatusCodes.Status201Created);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status404NotFound, ex.Message);
        }
    }

    [HttpPut("RestoreRoom/{id}")]
    public async Task<IActionResult> RestoreRoom(Guid id)
    {
        try
        {
            await _roomService.RevertSoftDelete(id);
            await _roomService.SaveChangesAsync();
            return StatusCode(StatusCodes.Status201Created);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status404NotFound, ex.Message);
        }
    }

    [HttpDelete("DeleteRoom/{id}")]
    public async Task<IActionResult> DeleteRoom(Guid id)
    {
        try
        {
            await _roomService.HardDelete(id);
            await _roomService.SaveChangesAsync();
            return StatusCode(StatusCodes.Status201Created);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status404NotFound, ex.Message);
        }
    }
}
