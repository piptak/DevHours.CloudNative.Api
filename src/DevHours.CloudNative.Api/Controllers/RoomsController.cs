using DevHours.CloudNative.Application.Commands;
using DevHours.CloudNative.Application.Data.Dtos;
using DevHours.CloudNative.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DevHours.CloudNative.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly IMediator mediator;

        public RoomsController(IMediator mediator)
            => (this.mediator) = (mediator);

        [HttpGet("{id:int}", Name = "GetRoom")]
        public async Task<ActionResult<RoomDto>> GetRoomAsync(int id, CancellationToken token = default)
        {
            return await mediator.Send(new GetRoomQuery { RoomId = id });
        }

        [HttpGet]
        public async Task<TableDto<RoomDto>> GetRooms([FromQuery] int skip, [FromQuery] int take)
        {
            return await mediator.Send(new GetRoomsQuery { Skip = skip, Take = take });
        }

        [HttpGet("{id}/bookings")]
        public async Task<ActionResult<TableDto<BookingDto>>> GetRoomBookingsAsync([FromRoute] int id, [FromQuery] int skip, [FromQuery] int take)
        {
            return await mediator.Send(new GetBookingsQuery { RoomId = id, Skip = skip, Take = take });
        }

        [HttpPost]
        public async Task<ActionResult<RoomDto>> AddRoom(RoomDto room, CancellationToken token = default)
        {
            var id =  await mediator.Send(new AddRoomCommand { RoomDto = room });
            return CreatedAtRoute("GetRoom", new { id = id }, null);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, RoomDto room, CancellationToken token = default)
        {
            if (room.Id != id)
            {
                throw new Exception("Id mismatch");
            }

            await mediator.Send(new UpdateRoomCommand { RoomDto = room });

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken token = default)
        {
            await mediator.Send(new DeleteRoomCommand { RoomId = id });
            return NoContent();
        }
    }
}