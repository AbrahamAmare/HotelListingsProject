using AutoMapper;
using HotelListingsApi.DTO;
using HotelListingsApi.Model;

namespace HotelListingsApi.Helpers
{
    public class MapperIntializer : Profile
    {
        public MapperIntializer()
        {
            CreateMap<Country, CountryDTO>().ReverseMap();
            CreateMap<Country, CreateCountryDTO>().ReverseMap();
            CreateMap<Hotel, HotelDTO>().ReverseMap();
            CreateMap<Hotel, CreateHotelDTO>().ReverseMap();
        }
    }
}