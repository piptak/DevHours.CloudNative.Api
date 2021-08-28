using AutoMapper;
using DevHours.CloudNative.Application.Data.Dtos;
using DevHours.CloudNative.Core.Exceptions;
using DevHours.CloudNative.Core.Repositories.Write;
using DevHours.CloudNative.Domain;
using DevHours.CloudNative.Shared.Abstraction.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace DevHours.CloudNative.Application.Commands
{
    public class AddBookingCommand : ICommand<int>
    {
        public BookingDto BookingDto { get; set; }

        public class AddBookingCommandHandler : ICommandHandler<AddBookingCommand, int>
        {
            private readonly IRoomBookingRepository repository;
            private readonly IMapper mapper;

            public AddBookingCommandHandler(IRoomBookingRepository repository, IMapper mapper) => (this.repository, this.mapper) = (repository, mapper);

            public async Task<int> Handle(AddBookingCommand command, CancellationToken cancellationToken)
            {
                var room = await repository.GetRoomAsync(command.BookingDto.RoomId);

                if (room is null)
                {
                    throw new RoomNotFoundException(command.BookingDto.RoomId);
                }

                var newBooking = mapper.Map<Booking>(command.BookingDto);

                room.AddBooking(newBooking);
                await repository.SaveChangesAsync(cancellationToken);

                return newBooking.Id;
            }
        }
    }
}
