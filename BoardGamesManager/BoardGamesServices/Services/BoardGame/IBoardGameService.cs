using System.Collections.Generic;
using System.Threading.Tasks;
using BoardGamesServices.DTOs;

namespace BoardGamesServices.Services.BoardGame
{
    public interface IBoardGameService
    {
        public IAsyncEnumerable<BoardGameDto> GetBoardGamesAsync(int? limit = null, int? page = null);
        public Task<int> GetBoardGamesCountAsync();
        public Task<BoardGameDto?> GetBoardGameForIdAsync(int boardGameId);
        public Task<BoardGameDto> DeleteBoardGameWithIdAsync(int boardGameId);
        public Task<BoardGameDto> AddBoardGameAsync(BoardGameDto boardGame);
        public Task<BoardGameDto> UpdateBoardGameAsync(BoardGameDto boardGame);
    }
}