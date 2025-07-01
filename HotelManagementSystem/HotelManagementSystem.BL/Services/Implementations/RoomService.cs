using AutoMapper;
using HotelManagementSystem.BL.DTOs.RoomDTO;
using HotelManagementSystem.BL.Services.Abstractions;
using HotelManagementSystem.Core.Entities;
using HotelManagementSystem.DL.Exceptions;
using HotelManagementSystem.DL.Repositories.Abstractions;
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

    public RoomService(IMapper mapper, IRoomReadRepository readRepository, IRoomWriteRepository writeRepository)
    {
        _readRepository = readRepository;
        _writeRepository = writeRepository;
        _mapper = mapper;
    }

    public async Task AddRoomAsync(AddRoomDTO addRoomDTO)
    {
        Room room = _mapper.Map<Room>(addRoomDTO);
        await _writeRepository.CreateAsync(room);
        //WARNING ADD ADDED BY

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
        Room room = await _readRepository.GetByIdAsync(Id);

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

        if (room is null)
        {
            throw new BaseException("Couldn't find room.");
        }
        if (!room.IsDeleted)
        {
            throw new BaseException("Room is already reverted.");
        }

        room.IsDeleted = false;

        //WARNING ADD DELETED BY

        _writeRepository.Update(room);
    }

    public Task<int> SaveChangesAsync()
    {
        throw new NotImplementedException();
    }

    public async Task SoftDelete(Guid Id)
    {
        Room room = await _readRepository.GetByIdAsync(Id);

        if (room is null)
        {
            throw new BaseException("Couldn't find room.");
        }
        if (room.IsDeleted)
        {
            throw new BaseException("Room is already deleted.");
        }

        room.IsDeleted = true;

        //WARNING ADD DELETED BY

        _writeRepository.Update(room);
    }

    public async Task Update(Guid Id, UpdateRoomDTO updateRoomDTO)
    {
        Room room = await _readRepository.GetByIdAsync(Id);

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

        //WARNING ADD DELETED BY

        _writeRepository.Update(updatedRoom);
    }
}
