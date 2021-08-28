namespace DevHours.CloudNative.Core.Exceptions
{
    public class BookingNotFoundException : DomainException
    {
        public BookingNotFoundException(int bookingId) : base($"Booking with id {bookingId} has not been found.")
        {

        }
    }
}
