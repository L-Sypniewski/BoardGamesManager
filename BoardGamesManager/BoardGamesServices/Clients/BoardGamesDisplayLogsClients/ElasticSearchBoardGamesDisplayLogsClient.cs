using System.Collections.Generic;
using BoardGamesServices.Model;

namespace BoardGamesServices.Clients.BoardGamesDisplayLogsClients
{
    public class ElasticSearchBoardGamesDisplayLogsClient: IBoardGamesDisplayLogsClient
    {
        public IAsyncEnumerable<BoardGamesDisplayLog> BoardGamesDisplayLogsFor(int boardGameId) => throw new System.NotImplementedException();
    }
}