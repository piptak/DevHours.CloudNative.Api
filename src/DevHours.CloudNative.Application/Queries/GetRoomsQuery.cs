using AutoMapper;
using DevHours.CloudNative.Application.Data.Dtos;
using DevHours.CloudNative.Core.Repositories.Read;
using DevHours.CloudNative.Shared.Abstraction.Queries;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DevHours.CloudNative.Application.Queries
{
    public class GetRoomsQuery : IQuery<TableDto<RoomDto>>
    {
        public int Skip { get; init; }
        public int Take { get; init; }

        public class GetBookingsQueryHandler : IQueryHandler<GetRoomsQuery, TableDto<RoomDto>>
        {
            private readonly IRoomBookingRepository repository;
            private readonly IMapper mapper;

            public GetBookingsQueryHandler(IRoomBookingRepository repository, IMapper mapper) => (this.repository, this.mapper) = (repository, mapper);

            public async Task<TableDto<RoomDto>> Handle(GetRoomsQuery query, CancellationToken cancellationToken)
            {
                var totalCount = await repository.TotalRoomsCountAsync();
                var rooms = await repository.QueryAsync(query.Skip, query.Take);
                var mappedRooms = mapper.Map<IEnumerable<RoomDto>>(rooms);

                return new TableDto<RoomDto> { TotalCount = totalCount, Values = mappedRooms };
            }
        }
    }
}
