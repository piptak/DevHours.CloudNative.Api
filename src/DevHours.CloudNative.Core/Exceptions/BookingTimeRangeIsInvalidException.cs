namespace DevHours.CloudNative.Core.Exceptions
{
    public class BookingTimeRangeIsInvalidException : DomainException
    {
        public BookingTimeRangeIsInvalidException(string message) : base(message)
        {

        }
    }
}
