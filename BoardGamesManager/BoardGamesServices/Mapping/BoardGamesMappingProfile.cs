using AutoMapper;
using BoardGamesServices.DTOs;
using Models;

namespace BoardGamesServices.Mapping
{
    public class BoardGamesMappingProfile : Profile
    {
        public BoardGamesMappingProfile()
        {
            CreateMap<BoardGame, BoardGameDto>();
            CreateMap<BoardGameDto, BoardGame>();
        }
    }
}