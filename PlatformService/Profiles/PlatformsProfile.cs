using AutoMapper;
using PlatformsService.Dtos;
using PlatformsService.Models;

namespace PlatformsService.Profiles
{ 
    public class PlatformsProfile : Profile
    {
        public PlatformsProfile()
        {
            // Source -> Target
            // Model to ReadDto - to send data to the client, 
            // we want to send a DTO, not the actual model
            CreateMap<Platform, PlatformReadDto>();
            // CreateMap<PlatformReadDto, Platform>(); // for testing purposes, to map back from ReadDto to Model

            // CreateDto to Model - for creating new entries in the database
            // we want to take data from the client in the form of a DTO and map it to our model
            CreateMap<PlatformCreateDto, Platform>();
        }
    }
}