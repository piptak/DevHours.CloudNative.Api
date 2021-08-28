using DevHours.CloudNative.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DevHours.CloudNative.Domain
{
    public class Room
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }
        public DateTimeOffset CreatedAt { get; set; }

        public void AddBooking(Booking bookingRequest)
        {
            BookingTimeRangePolicy(bookingRequest);
            Bookings.Add(bookingRequest);
        }

        public void UpdateBooking(Booking booking)
        {
            if (Bookings.SingleOrDefault(x => x.Id == booking.Id) is null)
            {
                throw new BookingNotFoundException(booking.Id);
            }

            BookingTimeRangePolicy(booking);
        }

        private void BookingTimeRangePolicy(Booking booking)
        {
            if (booking.EndDate < booking.StartDate)
            {
                throw new BookingTimeRangeIsInvalidException("Start and End dates must be in correct order.");
            }

            if (booking.StartDate < DateTime.UtcNow.Date)
            {
                throw new BookingTimeRangeIsInvalidException("Booking for passed days is not possible.");
            }

            bool intersectsWithExistingOne = Bookings.Where(b => b.Id != booking.Id && booking.TimeRane.IntersectsWith(b.TimeRane)).Any();

            if (intersectsWithExistingOne)
            {
                throw new BookingTimeRangeIsInvalidException("Unfortunately the room is already booked during this period.");
            }
        }

        public void UpdateByValuesFrom(Room room)
        {
            this.Description = room.Description;
        }
    }
}