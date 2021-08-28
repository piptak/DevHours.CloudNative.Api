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
    public class BookingsController : ControllerBase
    {
        private readonly IMediator mediator;

        public BookingsController(IMediator mediator) => (this.mediator) = (mediator);

        [HttpGet("{id:int}", Name = "GetBooking")]
        public async ValueTask<ActionResult<BookingDto>> GetBooking(int id, CancellationToken token = default)
        {
            return await mediator.Send(new GetBookingQuery { BookingId = id });
        }

        [HttpPost]
        public async Task<ActionResult<BookingDto>> Book(BookingDto booking, CancellationToken token = default)
        {
            var bookingId = await mediator.Send(new AddBookingCommand { BookingDto = booking });
            return CreatedAtRoute("GetBooking", new { id = bookingId }, null);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, BookingDto booking, CancellationToken token = default)
        {
            if (booking.Id != id)
            {
                throw new Exception("Id mismatch.");
            }

            await mediator.Send(new UpdateBookingCommand { BookingDto = booking });

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> CancelBooking(int id, CancellationToken token = default)
        {
            await mediator.Send(new DeleteBookingCommand { BookingId = id });
            return NoContent();
        }
    }
}