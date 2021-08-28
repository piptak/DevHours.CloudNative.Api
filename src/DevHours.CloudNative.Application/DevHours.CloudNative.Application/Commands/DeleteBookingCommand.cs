using AutoMapper;
using DevHours.CloudNative.Core.Exceptions;
using DevHours.CloudNative.Core.Repositories.Write;
using DevHours.CloudNative.Shared.Abstraction.Commands;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DevHours.CloudNative.Application.Commands
{
    public class DeleteBookingCommand : ICommand
    {
        public int BookingId { get; set; }

        public class DeleteBookingCommandHandler : ICommandHandler<DeleteBookingCommand>
        {
            private readonly IRoomBookingRepository repository;
            private readonly IMapper mapper;

            public DeleteBookingCommandHandler(IRoomBookingRepository repository, IMapper mapper) => (this.repository, this.mapper) = (repository, mapper);

            public async Task<Unit> Handle(DeleteBookingCommand command, CancellationToken cancellationToken)
            {
                var booking = await repository.GetBookingAsync(command.BookingId);
                if (booking is null)
                {
                    throw new BookingNotFoundException(command.BookingId);
                }

                await repository.DeleteBookingAsync(booking, cancellationToken);
                await repository.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
