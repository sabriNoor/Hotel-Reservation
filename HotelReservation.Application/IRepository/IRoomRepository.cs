using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelReservation.Domain.Entities;

namespace HotelReservation.Application.IRepository
{
    public interface IRoomRepository : IGenericRepository<Room>
    {
        Task<Room?> GetRoomDetailsAsync(Guid id);
        Task<List<Room>> GetAllRoomDetailsAsync();
    }
}