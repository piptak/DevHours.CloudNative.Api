namespace DevHours.CloudNative.Core.Exceptions
{
    public class RoomNotFoundException : DomainException
    {
        public RoomNotFoundException(int roomId) : base($"Room with id {roomId} has not been fround.")
        {

        }
    }
}
