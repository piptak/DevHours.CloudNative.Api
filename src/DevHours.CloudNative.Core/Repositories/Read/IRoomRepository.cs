using DevHours.CloudNative.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevHours.CloudNative.Core.Repositories.Read
{
    public interface IRoomRepository
    {
        Task<IEnumerable<Room>> QueryAsync(int skip, int take);
        Task<Room> GetRoomAsync(int id);
        Task<int> TotalRoomsCountAsync();
    }
}
