using Itenso.TimePeriod;
using System;

namespace DevHours.CloudNative.Domain
{
    public class Booking
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int RoomId { get; set; }
        public Room Room { get; set; }
        public ITimePeriod TimeRane => new TimeRange(StartDate, EndDate);
    }
}