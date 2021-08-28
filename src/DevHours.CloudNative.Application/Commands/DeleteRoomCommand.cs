using AutoMapper;
using DevHours.CloudNative.Core.Exceptions;
using DevHours.CloudNative.Core.Repositories.Write;
using DevHours.CloudNative.Shared.Abstraction.Commands;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DevHours.CloudNative.Application.Commands
{
    public class DeleteRoomCommand : ICommand
    {
        public int RoomId { get; set; }

        public class DeleteRoomCommandHandler : ICommandHandler<DeleteRoomCommand>
        {
            private readonly IRoomBookingRepository repository;
            private readonly IMapper mapper;

            public DeleteRoomCommandHandler(IRoomBookingRepository repository, IMapper mapper) => (this.repository, this.mapper) = (repository, mapper);

            public async Task<Unit> Handle(DeleteRoomCommand command, CancellationToken cancellationToken)
            {
                var room = await repository.GetRoomAsync(command.RoomId);
                if (room is null)
                {
                    throw new RoomNotFoundException(command.RoomId);
                }

                await repository.DeleteRoomAsync(room, cancellationToken);
                await repository.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
