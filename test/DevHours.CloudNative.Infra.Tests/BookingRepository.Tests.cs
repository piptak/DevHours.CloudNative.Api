using DevHours.CloudNative.DataAccess;
using DevHours.CloudNative.Domain;
using DevHours.CloudNative.Infra.Repositories.Write;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Xunit;

namespace DevHours.CloudNative.Infra.Tests
{
    public class BookingRepositoryTests
    {
        private RoomBookingRepository bookingRepository;
        private readonly HotelContext context;

        public BookingRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<HotelContext>()
                .UseInMemoryDatabase("HotelContextTest")
                .Options;
            context = new HotelContext(options);
        }

        [Fact]
        public async Task GetAsync_RetrieveDataWithId_ReturnsProperData()
        {
            //Arrange
            const int bookingId = 1;
            var expectedBooking = new Booking {
                Id = bookingId,
                Room = new Room(),
                RoomId = 1,
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddDays(1)
            };
            await context.Bookings.AddAsync(expectedBooking);
            await context.SaveChangesAsync();
            bookingRepository = new RoomBookingRepository(context);

            //Act
            var result = await bookingRepository.GetBookingAsync(bookingId);

            //Assert
            result.Should().BeEquivalentTo(expectedBooking);
        }
    }
}