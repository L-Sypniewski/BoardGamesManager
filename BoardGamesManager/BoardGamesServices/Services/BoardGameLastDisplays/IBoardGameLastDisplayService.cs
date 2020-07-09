using System.Collections.Generic;
using BoardGamesServices.DTOs;

namespace BoardGamesServices.Services.BoardGameLastDisplays
{
    public interface IBoardGameLastDisplayService
    {
        public IAsyncEnumerable<BoardGameLastDisplaysDto> LastDisplaysFor(int boardGameId, int limit);
    }
}