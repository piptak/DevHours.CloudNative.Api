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
    public class UpdateRoomCommand : ICommand
    {
        public RoomDto RoomDto { get; set; }

        public class UpdateRoomCommandHandler : ICommandHandler<UpdateRoomCommand>
        {
            private readonly IRoomBookingRepository repository;
            private readonly IMapper mapper;

            public UpdateRoomCommandHandler(IRoomBookingRepository repository, IMapper mapper) => (this.repository, this.mapper) = (repository, mapper);

            public async Task<Unit> Handle(UpdateRoomCommand command, CancellationToken cancellationToken)
            {
                var room = mapper.Map<Room>(command.RoomDto);

                var storedRoom = await repository.GetRoomAsync(room.Id);
                if (storedRoom is null)
                {
                    throw new RoomNotFoundException(room.Id);
                }

                storedRoom.UpdateByValuesFrom(room);
                await repository.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
