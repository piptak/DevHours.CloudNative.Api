using DevHours.CloudNative.Core.Repositories.Read;
using DevHours.CloudNative.DataAccess;
using DevHours.CloudNative.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevHours.CloudNative.Infra.Repositories.Read
{
    public class RoomBookingRepository : IRoomBookingRepository
    {
        private readonly HotelContext context;

        public RoomBookingRepository(HotelContext context)
        {
            this.context = context;
            this.context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public async Task<Booking> GetAsync(int id) => await context.Bookings.FindAsync(id);

        public async Task<IEnumerable<Booking>> QueryAsync(int roomId, int skip, int take) => await context.Bookings.Where(x => x.RoomId == roomId).Skip(skip).Take(take).ToListAsync();

        public async Task<IEnumerable<Room>> QueryAsync(int skip, int take) => await context.Rooms.Skip(skip).Take(take).ToListAsync();

        public async Task<Room> GetRoomAsync(int id) => await context.Rooms.FindAsync(id);
        public async Task<Booking> GetBookingAsync(int id) => await context.Bookings.FindAsync(id);

        public async Task<int> TotalBookingsCountAsync(int roomId) => await context.Bookings.CountAsync();

        public async Task<int> TotalRoomsCountAsync() => await context.Rooms.CountAsync();
    }
}
