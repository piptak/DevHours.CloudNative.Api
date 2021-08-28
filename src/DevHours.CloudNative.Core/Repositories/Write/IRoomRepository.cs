using DevHours.CloudNative.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace DevHours.CloudNative.Core.Repositories.Write
{
    public interface IRoomRepository
    {
        Task<Room> GetRoomAsync(int id);
        Task AddRoomAsync(Room newRoom, CancellationToken cancellationToken);
        Task DeleteRoomAsync(Room room, CancellationToken token);
    }
}
