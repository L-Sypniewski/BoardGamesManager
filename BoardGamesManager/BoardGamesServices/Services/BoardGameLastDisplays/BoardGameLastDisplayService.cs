using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BoardGamesServices.Clients.BoardGamesDisplayLogsClients;
using BoardGamesServices.DTOs;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace BoardGamesServices.Services.BoardGameLastDisplays
{
    public class BoardGameLastDisplayService : IBoardGameLastDisplayService
    {
        private readonly IBoardGamesDisplayLogsClient _boardGamesDisplayLogsClient;
        private readonly ILogger<BoardGameLastDisplayService> _logger;
        private readonly IMapper _mapper;

        public BoardGameLastDisplayService(IBoardGamesDisplayLogsClient boardGamesDisplayLogsClient,
                                           IMapper mapper,
                                           ILogger<BoardGameLastDisplayService>? logger = null)
        {
            _boardGamesDisplayLogsClient = boardGamesDisplayLogsClient;
            _mapper = mapper;
            _logger = logger ?? NullLogger<BoardGameLastDisplayService>.Instance;
        }

        public IAsyncEnumerable<BoardGameLastDisplaysDto> LastDisplaysFor(int boardGameId, int limit)
        {
            _logger.LogInformation("Fetching last {limit} BoardGameLastDisplays for boardGameId: {id}",
                                   limit, boardGameId);

            return _boardGamesDisplayLogsClient.BoardGamesDisplayLogsFor(boardGameId)
                                               .Take(limit)
                                               .Select(displayLog => _mapper.Map<BoardGameLastDisplaysDto>(displayLog));
        }
    }
}