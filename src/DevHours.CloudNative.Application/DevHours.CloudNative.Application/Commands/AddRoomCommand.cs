using AutoMapper;
using DevHours.CloudNative.Application.Data.Dtos;
using DevHours.CloudNative.Core.Repositories.Write;
using DevHours.CloudNative.Domain;
using DevHours.CloudNative.Shared.Abstraction.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace DevHours.CloudNative.Application.Commands
{
    public class AddRoomCommand : ICommand<int>
    {
        public RoomDto RoomDto { get; set; }

        public class AddRoomCommandHandler : ICommandHandler<AddRoomCommand, int>
        {
            private readonly IRoomBookingRepository repository;
            private readonly IMapper mapper;

            public AddRoomCommandHandler(IRoomBookingRepository repository, IMapper mapper) => (this.repository, this.mapper) = (repository, mapper);

            public async Task<int> Handle(AddRoomCommand command, CancellationToken cancellationToken)
            {
                var newRoom = mapper.Map<Room>(command.RoomDto);

                await repository.AddRoomAsync(newRoom, cancellationToken);
                await repository.SaveChangesAsync(cancellationToken);

                return newRoom.Id;
            }
        }
    }
}
