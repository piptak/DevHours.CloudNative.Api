using AutoMapper;
using DevHours.CloudNative.Application.Data.Dtos;
using DevHours.CloudNative.Core.Exceptions;
using DevHours.CloudNative.Core.Repositories.Read;
using DevHours.CloudNative.Shared.Abstraction.Queries;
using System.Threading;
using System.Threading.Tasks;

namespace DevHours.CloudNative.Application.Queries
{
    public class GetRoomQuery : IQuery<RoomDto>
    {
        public int RoomId { get; init; }

        public class GetBookingsQueryHandler : IQueryHandler<GetRoomQuery, RoomDto>
        {
            private readonly IRoomBookingRepository repository;
            private readonly IMapper mapper;

            public GetBookingsQueryHandler(IRoomBookingRepository repository, IMapper mapper) => (this.repository, this.mapper) = (repository, mapper);

            public async Task<RoomDto> Handle(GetRoomQuery query, CancellationToken cancellationToken)
            {
                var room = await repository.GetRoomAsync(query.RoomId);
                if (room is null)
                {
                    throw new RoomNotFoundException(query.RoomId);
                }

                return mapper.Map<RoomDto>(room);
            }
        }
    }
}
