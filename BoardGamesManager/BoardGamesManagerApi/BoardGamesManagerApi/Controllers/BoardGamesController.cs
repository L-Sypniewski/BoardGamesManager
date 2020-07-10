using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using BoardGamesManagerApi.Extensions;
using BoardGamesServices.DTOs;
using BoardGamesServices.Services.BoardGame;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Models;

namespace BoardGamesManagerApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BoardGamesController : ControllerBase
    {
        private readonly ILogger<BoardGamesController> _logger;

        public BoardGamesController(ILogger<BoardGamesController>? logger)
        {
            _logger = logger ?? NullLogger<BoardGamesController>.Instance;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGames([FromServices] IBoardGameService boardGameService)
        {
            var boardGamesDtos = await boardGameService.GetAllBoardGames().ToArrayAsync();

            return HttpContext.JsonShouldBeReturned()
                ? Ok(new {BoardGames = boardGamesDtos})
                : Ok(boardGamesDtos);
        }

    }
}