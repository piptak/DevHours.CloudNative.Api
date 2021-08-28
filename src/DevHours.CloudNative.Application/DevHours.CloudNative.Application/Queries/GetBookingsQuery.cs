using AutoMapper;
using DevHours.CloudNative.Application.Data.Dtos;
using DevHours.CloudNative.Core.Repositories.Read;
using DevHours.CloudNative.Shared.Abstraction.Queries;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DevHours.CloudNative.Application.Queries
{
    public class GetBookingsQuery : IQuery<TableDto<BookingDto>>
    {
        public int RoomId { get; set; }
        public int Skip { get; init; }
        public int Take { get; init; }

        public class GetBookingsQueryHandler : IQueryHandler<GetBookingsQuery, TableDto<BookingDto>>
        {
            private readonly IRoomBookingRepository repository;
            private readonly IMapper mapper;

            public GetBookingsQueryHandler(IRoomBookingRepository repository, IMapper mapper) => (this.repository, this.mapper) = (repository, mapper);

            public async Task<TableDto<BookingDto>> Handle(GetBookingsQuery query, CancellationToken cancellationToken)
            {
                var totalCount = await repository.TotalBookingsCountAsync(query.RoomId);
                var bookings = await repository.QueryAsync(query.RoomId, query.Skip, query.Take);
                var mappedBookings = mapper.Map<IEnumerable<BookingDto>>(bookings);

                return new TableDto<BookingDto> { TotalCount = totalCount, Values = mappedBookings };
            }
        }
    }
}
