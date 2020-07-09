using System.Collections.Generic;
using BoardGamesServices.Model;

namespace BoardGamesServices.Clients.BoardGamesDisplayLogsClients
{
    public interface IBoardGamesDisplayLogsClient
    {
        public IAsyncEnumerable<BoardGamesDisplayLog> BoardGamesDisplayLogsFor(int boardGameId);
    }
}