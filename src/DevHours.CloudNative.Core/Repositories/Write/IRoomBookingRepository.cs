using System.Threading;
using System.Threading.Tasks;

namespace DevHours.CloudNative.Core.Repositories.Write
{
    public interface IRoomBookingRepository : IBookingRepository, IRoomRepository
    {
        Task SaveChangesAsync(CancellationToken cancellationToken);        
    }
}
