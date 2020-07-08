using AutoMapper;
using BoardGamesServices.DTOs;
using BoardGamesServices.Model;
using Models;

namespace BoardGamesServices.Mapping
{
    public class ApplicationLogMappingProfile: Profile
    {
        public ApplicationLogMappingProfile() {
            CreateMap<BoardGamesDisplayLog, BoardGameLastDisplaysDto>();
            CreateMap<BoardGameLastDisplaysDto, BoardGamesDisplayLog>();
        }
    }
}