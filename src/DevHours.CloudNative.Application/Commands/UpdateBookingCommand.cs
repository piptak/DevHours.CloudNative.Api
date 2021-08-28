using AutoMapper;
using DevHours.CloudNative.Application.Data.Dtos;
using DevHours.CloudNative.Core.Exceptions;
using DevHours.CloudNative.Core.Repositories.Write;
using DevHours.CloudNative.Domain;
using DevHours.CloudNative.Shared.Abstraction.Commands;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DevHours.CloudNative.Application.Commands
{
    public class UpdateBookingCommand : ICommand
    {
        public BookingDto BookingDto { get; set; }

        public class UpdateBookingCommandHandler : ICommandHandler<UpdateBookingCommand>
        {
            private readonly IRoomBookingRepository repository;
            private readonly IMapper mapper;

            public UpdateBookingCommandHandler(IRoomBookingRepository repository, IMapper mapper) => (this.repository, this.mapper) = (repository, mapper);

            public async Task<Unit> Handle(UpdateBookingCommand command, CancellationToken cancellationToken)
            {
                var booking = mapper.Map<Booking>(command.BookingDto);

                var storedRoom = await repository.GetRoomAsync(booking.RoomId);
                if (storedRoom is null)
                {
                    throw new RoomNotFoundException(booking.RoomId);
                }

                storedRoom.UpdateBooking(booking);
                await repository.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
