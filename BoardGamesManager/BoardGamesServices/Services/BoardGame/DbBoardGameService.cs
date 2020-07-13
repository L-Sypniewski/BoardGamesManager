using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BoardGamesManagerCore.Extensions;
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
        private readonly ILogger<DbBoardGameService> _logger;
        private readonly IMapper _mapper;

        public DbBoardGameService(BoardGamesDbContext dbContext, IMapper mapper, ILogger<DbBoardGameService>? logger = null)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger ?? NullLogger<DbBoardGameService>.Instance;
        }

        public IAsyncEnumerable<BoardGameDto> GetBoardGamesAsync(int? limit = null, int? page = null)
        {
            _logger.LogInformation("Getting all BoardGames from a database");

            try
            {
                return _dbContext.BoardGames
                                 .AsNoTracking()
                                 .OrderBy(boardGame => boardGame.BoardGameId)
                                 .Paginated(limit, page)
                                 .AsAsyncEnumerable()
                                 .Select(boardGame => _mapper.Map<BoardGameDto>(boardGame));
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Exception has been thrown");
                throw;
            }
        }

        public async Task<bool> BoardGameExists(int boardGameId)
        {
            IQueryable<Models.BoardGame> boardGames = _dbContext.BoardGames;
            return await boardGames.AnyAsync(boardGame => boardGame.BoardGameId == boardGameId);
        }

        public async Task<int> GetBoardGamesCountAsync()
        {
            _logger.LogInformation("Getting total number of all BoardGames");

            return await _dbContext.BoardGames
                                   .AsNoTracking()
                                   .CountAsync();
        }

        public async Task<BoardGameDto?> GetBoardGameForIdAsync(int boardGameId)
        {
            _logger.LogInformation("Getting BoardGame with id {id} from a database", boardGameId);

            try
            {
                var boardGame = await _dbContext.BoardGames
                                                .FindAsync(boardGameId);

                if (boardGame == null) return null;
                return _mapper.Map<BoardGameDto>(boardGame);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Exception has been thrown");
                throw;
            }
        }

        public async Task<BoardGameDto> DeleteBoardGameWithIdAsync(int boardGameId)
        {
            _logger.LogInformation("Deleting BoardGame with id {id} from a database", boardGameId);

            try
            {
                var boardGame = await _dbContext.BoardGames
                                                .FindAsync(boardGameId);
                _dbContext.BoardGames.Remove(boardGame);
                await _dbContext.SaveChangesAsync();

                return _mapper.Map<BoardGameDto>(boardGame);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Exception has been thrown");
                throw;
            }
        }

        public async Task<BoardGameDto> AddBoardGameAsync(BoardGameDto boardGame)
        {
            _logger.LogInformation("Adding BoardGame {boardGame} to a database", boardGame);

            try
            {
                var boardGameEntity = _mapper.Map<Models.BoardGame>(boardGame);
                await _dbContext.BoardGames.AddAsync(boardGameEntity);
                await _dbContext.SaveChangesAsync();

                return _mapper.Map<BoardGameDto>(boardGame);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Exception has been thrown");
                throw;
            }
        }

        public async Task<BoardGameDto> UpdateBoardGameAsync(BoardGameDto boardGame)
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