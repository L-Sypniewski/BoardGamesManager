using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BoardGamesManagerApi.Extensions;
using BoardGamesManagerApi.Model;
using BoardGamesServices.Services.BoardGame;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace BoardGamesManagerApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BoardGamesController : ControllerBase
    {
        private const int _maxPageSize = 500;

        private readonly ILogger<BoardGamesController> _logger;

        public BoardGamesController(ILogger<BoardGamesController>? logger)
        {
            _logger = logger ?? NullLogger<BoardGamesController>.Instance;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGames([FromServices] IBoardGameService boardGameService, [FromQuery] PaginationQuery paginationQuery)
        {
            _logger.LogInformation("Getting all board games, pagination: {pagination}", paginationQuery);

            var boardGamesCount = await boardGameService.GetBoardGamesCountAsync();
            var pagination = paginationQuery.ToPagination(boardGamesCount, _maxPageSize);

            var boardGamesDtos = await boardGameService.GetBoardGamesAsync(pagination.PageSize, pagination.Page)
                                                       .ToArrayAsync();

            Response.AddPaginationHeaders(pagination);
            return HttpContext.JsonShouldBeReturned() ? Ok(new {BoardGames = boardGamesDtos}) : Ok(boardGamesDtos);
        }

        [HttpGet]
        [Route("{boardGameId:int}")]
        public async Task<IActionResult> GetGameDetails([FromServices] IBoardGameService boardGameService, [FromRoute] [Range(1, int.MaxValue)] int boardGameId)
        {
            _logger.LogInformation("Getting board game details for id: {gameId}", boardGameId);

            var boardGame = await boardGameService.GetBoardGameForIdAsync(boardGameId);
            if (boardGame.HasValue) return Ok(boardGame.Value);
            return NotFound($"BoardGame for id {boardGameId} does not exist");
        }
    }
}