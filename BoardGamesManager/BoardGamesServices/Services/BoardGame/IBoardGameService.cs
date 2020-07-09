using System.Collections.Generic;
using System.Threading.Tasks;
using BoardGamesServices.DTOs;

namespace BoardGamesServices.Services.BoardGame
{
    public interface IBoardGameService
    {
        public IAsyncEnumerable<BoardGameDto> GetAllBoardGames();
        public Task<BoardGameDto> GetBoardGameForId(string boardGameId);
        public Task<BoardGameDto> DeleteBoardGameWithId(string boardGameId);
        public Task<BoardGameDto> AddBoardGame(BoardGameDto boardGame);
        public Task<BoardGameDto> UpdateBoardGame(BoardGameDto boardGame);
    }
}