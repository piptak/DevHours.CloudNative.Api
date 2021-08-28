using AutoMapper;
using DevHours.CloudNative.Application.Data.Dtos;
using DevHours.CloudNative.Core.Exceptions;
using DevHours.CloudNative.Core.Repositories.Read;
using DevHours.CloudNative.Shared.Abstraction.Queries;
using System.Threading;
using System.Threading.Tasks;

namespace DevHours.CloudNative.Application.Queries
{
    public class GetBookingQuery : IQuery<BookingDto>
    {
        public int BookingId { get; set; }
    }

    public class GetBookingsQueryHandler : IQueryHandler<GetBookingQuery, BookingDto>
    {
        private readonly IRoomBookingRepository repository;
        private readonly IMapper mapper;

        public GetBookingsQueryHandler(IRoomBookingRepository repository, IMapper mapper) => (this.repository, this.mapper) = (repository, mapper);

        public async Task<BookingDto> Handle(GetBookingQuery query, CancellationToken cancellationToken)
        {
            var booking = await repository.GetBookingAsync(query.BookingId);
            if (booking is null)
            {
                throw new BookingNotFoundException(query.BookingId);
            }

            return mapper.Map<BookingDto>(booking);
        }
    }
}
