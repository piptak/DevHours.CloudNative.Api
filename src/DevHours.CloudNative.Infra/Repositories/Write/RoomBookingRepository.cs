using DevHours.CloudNative.Core.Repositories.Write;
using DevHours.CloudNative.DataAccess;
using DevHours.CloudNative.Domain;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace DevHours.CloudNative.Infra.Repositories.Write
{
    public class RoomBookingRepository : IRoomBookingRepository
    {
        private readonly HotelContext context;

        public RoomBookingRepository(HotelContext context)
        {
            this.context = context;
            this.context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.TrackAll;
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await context.SaveChangesAsync();
        }

        public async Task DeleteBookingAsync(Booking booking, CancellationToken token)
        {
            await Task.CompletedTask;
            context.Bookings.Remove(booking);
        }

        public async Task DeleteRoomAsync(Room room, CancellationToken token)
        {
            await Task.CompletedTask;
            context.Rooms.Remove(room);
        }

        public async Task<Room> GetRoomAsync(int id) => await context.Rooms.Include(x => x.Bookings).SingleOrDefaultAsync(x => x.Id == id);

        public async Task AddRoomAsync(Room room, CancellationToken cancellationToken)
        {
            await context.Rooms.AddAsync(room);
        }

        public async Task<Booking> GetBookingAsync(int bookingId) => await context.Bookings.FindAsync(bookingId);
    }
}
