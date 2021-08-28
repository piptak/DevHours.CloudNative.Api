using DevHours.CloudNative.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace DevHours.CloudNative.Core.Repositories.Write
{
    public interface IBookingRepository
    {
        Task DeleteBookingAsync(Booking booking, CancellationToken token);
        Task<Booking> GetBookingAsync(int bookingId);
    }
}
