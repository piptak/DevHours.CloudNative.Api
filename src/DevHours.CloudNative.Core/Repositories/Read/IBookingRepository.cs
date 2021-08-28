using DevHours.CloudNative.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevHours.CloudNative.Core.Repositories.Read
{
    public interface IBookingRepository
    {
        Task<IEnumerable<Booking>> QueryAsync(int roomId, int skip, int take);
        Task<Booking> GetBookingAsync(int id);
        Task<int> TotalBookingsCountAsync(int roomId);
    }
}
