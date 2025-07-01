using AutoMapper;
using HotelManagementSystem.BL.DTOs.ReservationDTO;
using HotelManagementSystem.BL.Services.Abstractions;
using HotelManagementSystem.Core.Entities;
using HotelManagementSystem.Core.Entities.Identity;
using HotelManagementSystem.DL.Exceptions;
using HotelManagementSystem.DL.Repositories.Abstractions;
using System.Linq.Expressions;

namespace HotelManagementSystem.BL.Services.Implementations;

public class ReservationService : IReservationService
{
    readonly IReservationReadRepository _readRepository;
    readonly IReservationWriteRepository _writeRepository;
    readonly IMapper _mapper;
    readonly IAuthService _authService;

    public ReservationService(IAuthService authService, IMapper mapper, IReservationReadRepository readRepository, IReservationWriteRepository writeRepository)
    {
        _readRepository = readRepository;
        _writeRepository = writeRepository;
        _mapper = mapper;
        _authService = authService;
    }

    public async Task AddReservationAsync(AddReservationDTO addReservationDTO)
    {
        Reservation reservation = _mapper.Map<Reservation>(addReservationDTO);
        AppUser user = await _authService.GetCurrentUserAsync();

        if (user is null)
        {
            throw new BaseException("Please login.");
        }

        reservation.CreatedBy = user;
        reservation.CreatedAt = DateTime.Now;

        await _writeRepository.CreateAsync(reservation);

    }

    public async Task<ICollection<GetReservationDTO>> GetAllReservations()
    {
        return _mapper.Map<ICollection<GetReservationDTO>>(await _readRepository.GetAllAsync());
    }

    public async Task<GetReservationDTO> GetReservationByCondition(Expression<Func<Reservation, bool>> expression)
    {
        Reservation reservation = await _readRepository.GetOneByCondition(expression);

        return _mapper.Map<GetReservationDTO>(reservation);
    }

    public async Task<GetReservationDTO> GetReservationById(Guid Id)
    {
        Reservation reservation = await _readRepository.GetByIdAsync(Id, false, "Services", "Room", "Customer");

        if (reservation is null)
        {
            throw new BaseException("Couldn't find reservation.");
        }

        return _mapper.Map<GetReservationDTO>(reservation);
    }

    public async Task HardDelete(Guid Id)
    {
        Reservation reservation = await _readRepository.GetByIdAsync(Id);

        if (reservation is null)
        {
            throw new BaseException("Couldn't find reservation.");
        }

        _writeRepository.Delete(reservation);
    }

    public async Task RevertSoftDelete(Guid Id)
    {
        Reservation reservation = await _readRepository.GetByIdAsync(Id);

        AppUser user = await _authService.GetCurrentUserAsync();

        if (user is null)
        {
            throw new BaseException("Please login.");
        }
        if (reservation is null)
        {
            throw new BaseException("Couldn't find reservation.");
        }
        if (!reservation.IsDeleted)
        {
            throw new BaseException("Reservation is already reverted.");
        }

        reservation.IsDeleted = false;
        reservation.DeletedBy = null;
        reservation.DeletedAt = null;

        _writeRepository.Update(reservation);
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _writeRepository.SaveChangesAsync();
    }

    public async Task SoftDelete(Guid Id)
    {
        Reservation reservation = await _readRepository.GetByIdAsync(Id);

        AppUser user = await _authService.GetCurrentUserAsync();

        if (user is null)
        {
            throw new BaseException("Please login.");
        }
        if (reservation is null)
        {
            throw new BaseException("Couldn't find reservation.");
        }
        if (reservation.IsDeleted)
        {
            throw new BaseException("Reservation is already deleted.");
        }

        reservation.IsDeleted = true;
        reservation.DeletedBy = user;
        reservation.DeletedAt = DateTime.Now;

        _writeRepository.Update(reservation);
    }

    public async Task Update(Guid Id, UpdateReservationDTO updateReservationDTO)
    {
        Reservation reservation = await _readRepository.GetByIdAsync(Id);

        AppUser user = await _authService.GetCurrentUserAsync();

        if (user is null)
        {
            throw new BaseException("Please login.");
        }
        if (reservation is null)
        {
            throw new BaseException("Couldn't find reservation.");
        }
        if (reservation.IsDeleted)
        {
            throw new BaseException("Reservation is already deleted.");
        }

        Reservation updatedReservation = _mapper.Map<Reservation>(updateReservationDTO);

        updatedReservation.Id = Id;
        updatedReservation.CreatedBy = reservation.CreatedBy;
        updatedReservation.CreatedAt = reservation.CreatedAt;
        updatedReservation.UpdatedAt = DateTime.Now;
        updatedReservation.UpdatedBy = user;

        _writeRepository.Update(updatedReservation);
    }
}
