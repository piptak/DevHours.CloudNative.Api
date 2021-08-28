using AutoMapper;
using DevHours.CloudNative.Application.Queries;
using DevHours.CloudNative.Core.Exceptions;
using DevHours.CloudNative.Core.Repositories.Read;
using DevHours.CloudNative.Domain;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace DevHours.CloudNative.Core.Test
{
    public class RoomBookingServiceTests
    {

        private readonly Mock<IRoomBookingRepository> repository;
        private readonly Mock<IMapper> mapper;

        public RoomBookingServiceTests()
        {
            repository = new Mock<IRoomBookingRepository>();
            mapper = new Mock<IMapper>();
        }

        [Fact]
        public async Task Book_NoRoomExists_ShouldThrowException()
        {
            //Arrange
            var notExistingBookingRoom = new Booking();
            GetBookingQuery query = new GetBookingQuery();
            GetBookingsQueryHandler handler = new GetBookingsQueryHandler(repository.Object, mapper.Object);

            repository
                .Setup(b => b.GetBookingAsync(It.IsAny<int>()))
                .Returns(() => Task.FromResult<Booking>(null));

            //Act
            await Assert.ThrowsAsync<BookingNotFoundException>(async () => await handler.Handle(query, It.IsAny<CancellationToken>()));
        }
    }
}
