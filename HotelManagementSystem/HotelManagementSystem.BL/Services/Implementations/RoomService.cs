using AutoMapper;
using HotelManagementSystem.BL.DTOs.RoomDTO;
using HotelManagementSystem.BL.ExternalServices.Abstractions;
using HotelManagementSystem.BL.Services.Abstractions;
using HotelManagementSystem.Core.Entities;
using HotelManagementSystem.Core.Entities.Identity;
using HotelManagementSystem.DL.Exceptions;
using HotelManagementSystem.DL.Repositories.Abstractions;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystem.BL.Services.Implementations;

public class RoomService : IRoomService
{
    readonly IRoomReadRepository _readRepository;
    readonly IRoomWriteRepository _writeRepository;
    readonly IMapper _mapper;
    readonly IAuthService _authService;
    readonly IFileUploadService _fileUploadService;
    IWebHostEnvironment _env;

    public RoomService(IAuthService authService, IMapper mapper, IRoomReadRepository readRepository, IRoomWriteRepository writeRepository, IFileUploadService fileUploadService, IWebHostEnvironment env)
    {
        _authService = authService;
        _readRepository = readRepository;
        _writeRepository = writeRepository;
        _mapper = mapper;
        _fileUploadService = fileUploadService;
        _env = env;
    }

    public async Task AddRoomAsync(AddRoomDTO addRoomDTO)
    {
        Room room = _mapper.Map<Room>(addRoomDTO);
        
        //AppUser user = await _authService.GetCurrentUserAsync();

        //if (user is null)
        //{
        //    throw new BaseException("Please login.");
        //}

        //room.CreatedBy = user;
        room.CreatedAt = DateTime.Now;
        room.RoomImageURL = await _fileUploadService.SaveFileAsync(addRoomDTO.Image, _env.WebRootPath, new[] { ".jpg", ".jpeg", ".webp", ".png" });

        await _writeRepository.CreateAsync(room);
        

    }

    public async Task<ICollection<GetRoomDTO>> GetAllRooms()
    {
        return _mapper.Map<ICollection<GetRoomDTO>>(await _readRepository.GetAllAsync());
    }

    public async Task<GetRoomDTO> GetRoomByCondition(Expression<Func<Room, bool>> expression)
    {
        Room room = await _readRepository.GetOneByCondition(expression);

        return _mapper.Map<GetRoomDTO>(room);
    }

    public async Task<GetRoomDTO> GetRoomById(Guid Id)
    {
        Room room = await _readRepository.GetByIdAsync(Id, false, "Reservations");

        if (room is null)
        {
            throw new BaseException("Couldn't find room.");
        }

        return _mapper.Map<GetRoomDTO>(room);
    }

    public async Task HardDelete(Guid Id)
    {
        Room room = await _readRepository.GetByIdAsync(Id);

        if (room is null)
        {
            throw new BaseException("Couldn't find room.");
        }

        _writeRepository.Delete(room);
    }

    public async Task RevertSoftDelete(Guid Id)
    {
        Room room = await _readRepository.GetByIdAsync(Id);

        //AppUser user = await _authService.GetCurrentUserAsync();

        //if (user is null)
        //{
        //    throw new BaseException("Please login.");
        //}
        if (room is null)
        {
            throw new BaseException("Couldn't find room.");
        }
        if (!room.IsDeleted)
        {
            throw new BaseException("Room is already reverted.");
        }

        room.IsDeleted = false;
        room.DeletedBy = null;
        room.DeletedAt = null;

        _writeRepository.Update(room);
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _writeRepository.SaveChangesAsync();
    }

    public async Task SoftDelete(Guid Id)
    {
        Room room = await _readRepository.GetByIdAsync(Id);

        //AppUser user = await _authService.GetCurrentUserAsync();

        //if (user is null)
        //{
        //    throw new BaseException("Please login.");
        //}
        if (room is null)
        {
            throw new BaseException("Couldn't find room.");
        }
        if (room.IsDeleted)
        {
            throw new BaseException("Room is already deleted.");
        }

        room.IsDeleted = true;
        //room.DeletedBy = user;
        room.DeletedAt = DateTime.Now;

        _writeRepository.Update(room);
    }

    public async Task Update(Guid Id, UpdateRoomDTO updateRoomDTO)
    {
        Room room = await _readRepository.GetByIdAsync(Id);

        //AppUser user = await _authService.GetCurrentUserAsync();

        //if (user is null)
        //{
        //    throw new BaseException("Please login.");
        //}
        if (room is null)
        {
            throw new BaseException("Couldn't find room.");
        }
        if (room.IsDeleted)
        {
            throw new BaseException("Room is already deleted.");
        }

        Room updatedRoom = _mapper.Map<Room>(updateRoomDTO);

        updatedRoom.Id = Id;
        //updatedRoom.CreatedBy = room.CreatedBy;
        updatedRoom.CreatedAt = room.CreatedAt;
        updatedRoom.UpdatedAt = DateTime.Now;
        //updatedRoom.UpdatedBy = user;
        updatedRoom.RoomImageURL = await _fileUploadService.SaveFileAsync(updateRoomDTO.Image, _env.WebRootPath, new[] { ".jpg", ".jpeg", ".webp", ".png" });

        _writeRepository.Update(updatedRoom);
    }
}
