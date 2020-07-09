using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BoardGamesServices.DTOs;
using EfCoreData.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace BoardGamesServices.Services.BoardGame
{
    public class DbBoardGameService : IBoardGameService
    {
        private readonly BoardGamesDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<DbBoardGameService> _logger;

        public DbBoardGameService(BoardGamesDbContext dbContext, IMapper mapper, ILogger<DbBoardGameService>? logger = null)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger ?? NullLogger<DbBoardGameService>.Instance;
        }

        public IAsyncEnumerable<BoardGameDto> GetAllBoardGames()
        {
            _logger.LogInformation("Getting all BoardGames from a database");

            try
            {
                return _dbContext.BoardGames
                                 .AsNoTracking()
                                 .AsAsyncEnumerable()
                                 .Select(boardGame => _mapper.Map<BoardGameDto>(boardGame));
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Exception has been thrown");
                throw;
            }
        }

        public async Task<BoardGameDto> GetBoardGameForId(string boardGameId)
        {
            _logger.LogInformation("Getting BoardGame with id {id} from a database", boardGameId);

            try
            {
                var boardGame = await _dbContext.BoardGames
                                                .FindAsync(boardGameId);
                return _mapper.Map<BoardGameDto>(boardGame);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Exception has been thrown");
                throw;
            }
        }

        public async Task<BoardGameDto> DeleteBoardGameWithId(string boardGameId)
        {
            _logger.LogInformation("Deleting BoardGame with id {id} from a database", boardGameId);

            try
            {
                var boardGame = await _dbContext.BoardGames
                                                .FindAsync(boardGameId);
                _dbContext.BoardGames.Remove(boardGame);

                return _mapper.Map<BoardGameDto>(boardGame);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Exception has been thrown");
                throw;
            }
        }

        public async Task<BoardGameDto> AddBoardGame(BoardGameDto boardGame)
        {
            _logger.LogInformation("Adding BoardGame {boardGame} to a database", boardGame);

            try
            {
                var boardGameEntity = _mapper.Map<Models.BoardGame>(boardGame);
                await _dbContext.BoardGames.AddAsync(boardGameEntity);

                return _mapper.Map<BoardGameDto>(boardGame);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Exception has been thrown");
                throw;
            }
        }

        public async Task<BoardGameDto> UpdateBoardGame(BoardGameDto boardGame)
        {
            _logger.LogInformation("Updating BoardGame {boardGame} to a database", boardGame);

            try
            {
                var boardGameEntity = await _dbContext.BoardGames
                                                      .FindAsync(boardGame.BoardGameId);
                boardGameEntity.Name = boardGame.Name;
                boardGameEntity.MaxPlayers = boardGame.MaxPlayers;
                boardGameEntity.MinPlayers = boardGame.MinPlayers;
                boardGameEntity.MinRecommendedAge = boardGame.MinRecommendedAge;
                await _dbContext.SaveChangesAsync();

                return _mapper.Map<BoardGameDto>(boardGameEntity);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Exception has been thrown");
                throw;
            }
        }
    }
}