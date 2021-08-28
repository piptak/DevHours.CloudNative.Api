using AutoMapper;
using DevHours.CloudNative.Application.Data.Dtos;
using DevHours.CloudNative.Domain;

namespace DevHours.CloudNative.Application.Data.Dtos.Mappings
{
    public class DtoMappingProfile : Profile
    {
        public DtoMappingProfile()
        {
            AllowNullCollections = true;
            CreateMap<RoomDto, Room>();
            CreateMap<Room, RoomDto>();
            CreateMap<BookingDto, Booking>();
            CreateMap<Booking, BookingDto>();
        }
    }
}