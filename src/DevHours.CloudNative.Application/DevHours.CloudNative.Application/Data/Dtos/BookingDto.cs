using System;

namespace DevHours.CloudNative.Application.Data.Dtos
{
    public class BookingDto
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int RoomId { get; set; }
    }
}