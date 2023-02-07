using AutoMapper;
using MapperApp.Models;
using MapperApp.Models.DTOs.Incoming;
using MapperApp.Models.DTOs.Outgoing;

namespace MapperApp.Profiles
{
    public class DriverProfile : Profile
    {
        public DriverProfile()
        {
            // Map from input to a Driver Model
            CreateMap<CreateDriverDto, Driver>()
                .ForMember(
                    // Expression indicating the destination member to map
                    dest => dest.Id,
                    // Expression indicating the source data
                    opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(
                    dest => dest.FirstName,
                    // Example of formatting incoming data automatically
                    opt => opt.MapFrom(src => src.FirstName.ToUpper()))
                .ForMember(
                    dest => dest.LastName, 
                    opt => opt.MapFrom(src => src.LastName))
                .ForMember(
                    dest => dest.DriverNumber,
                    opt => opt.MapFrom(src => src.DriverNumber))
                .ForMember(
                    dest => dest.WorldChampionships,
                    opt => opt.MapFrom(src => src.WorldChampionships))
                .ForMember(
                    dest => dest.Status,
                    opt => opt.MapFrom(src => 1))
                .ForMember(
                    dest => dest.DateAdded,
                    opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(
                    dest => dest.DateUpdated,
                    opt => opt.MapFrom(src => DateTime.UtcNow))
                ;

            // Map for Driver to output 
            CreateMap<Driver, DriverDto>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(src => src.Id))
                .ForMember(
                    dest => dest.FullName,
                    opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
                .ForMember(
                    dest => dest.DriverNumber,
                    opt => opt.MapFrom(src => src.DriverNumber))
                .ForMember(
                    dest => dest.WorldChampionships,
                    opt => opt.MapFrom(src => src.WorldChampionships))
                ;
        }

    }
}
