using AutoMapper;
using BoardGamesServices.DTOs;

namespace BoardGamesManagerMvc.Models.Mapping
{
    public class BoardGamesViewModelMappingProfile : Profile
    {
        public BoardGamesViewModelMappingProfile()
        {
            CreateMap<BoardGameViewModel, BoardGameDto>();
            CreateMap<BoardGameDto, BoardGameViewModel>();
        }
    }
}