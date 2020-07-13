using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BoardGamesManagerMvc.Models;
using BoardGamesServices.DTOs;
using BoardGamesServices.Services.BoardGame;
using BoardGamesServices.Services.BoardGameLastDisplays;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BoardGamesManagerMvc.Controllers
{
    public class BoardGamesController : Controller
    {
        private readonly IMapper _mapper;

        public BoardGamesController(IMapper mapper)
        {
            _mapper = mapper;
        }

        // GET: BoardGames
        public async Task<IActionResult> Index([FromServices] IBoardGameService boardGameService)
        {
            var boardGameDto = await boardGameService.GetBoardGamesAsync().ToArrayAsync();
            var viewModels = boardGameDto
                .Select(dto => _mapper.Map<BoardGameDto, BoardGameViewModel>(dto));

            return View(viewModels);
        }

        // GET: BoardGames/Details/5
        public async Task<IActionResult> Details([FromServices] IBoardGameService boardGameService,
                                                 [FromServices] IBoardGameLastDisplayService lastDisplayService,
                                                 int? id)
        {
            if (id == null) return NotFound();

            var boardGame = await boardGameService.GetBoardGameForIdAsync(id.Value);

            if (boardGame == null) return NotFound();

            const int lastDisplaysLimit = 10; // should be set via config file, we don't like magic strings
            var boardGameDto = boardGame.Value;
            var boardGameId = boardGameDto.BoardGameId;
            var lastDisplays = await lastDisplayService.LastDisplaysFor(boardGameId, lastDisplaysLimit)
                                                       .Select(lastDisplay => new LastDisplayViewModel(lastDisplay.Source, lastDisplay.DisplayDatetime))
                                                       .ToArrayAsync();

            var viewModel = new BoardGameWithLastDisplaysViewModel
            {
                BoardGameId = boardGameId,
                Name = boardGameDto.Name,
                MinPlayers = boardGameDto.MinPlayers,
                MaxPlayers = boardGameDto.MaxPlayers,
                MinRecommendedAge = boardGameDto.MinRecommendedAge,
                LastDisplays = lastDisplays
            };
            return View(viewModel);
        }

        // GET: BoardGames/Create
        public IActionResult Create() => View();

        // POST: BoardGames/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromServices] IBoardGameService boardGameService,
                                                [Bind("BoardGameId,Name,MinPlayers,MaxPlayers,MinRecommendedAge")]
                                                BoardGameViewModel boardGame)
        {
            if (!ModelState.IsValid) return View(boardGame);

            var dto = _mapper.Map<BoardGameViewModel, BoardGameDto>(boardGame);
            await boardGameService.AddBoardGameAsync(dto);

            return RedirectToAction(nameof(Index));
        }

        // GET: BoardGames/Edit/5
        public async Task<IActionResult> Edit([FromServices] IBoardGameService boardGameService, int? id)
        {
            if (id == null) return NotFound();

            var boardGame = await boardGameService.GetBoardGameForIdAsync(id.Value);
            if (boardGame == null) return NotFound();

            var viewModel = _mapper.Map<BoardGameDto, BoardGameViewModel>(boardGame.Value);
            return View(viewModel);
        }

        // POST: BoardGames/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromServices] IBoardGameService boardGameService,
                                              int id, [Bind("BoardGameId,Name,MinPlayers,MaxPlayers,MinRecommendedAge")]
                                              BoardGameViewModel boardGame)
        {
            if (id != boardGame.BoardGameId) return NotFound();

            if (!ModelState.IsValid) return View(boardGame);

            try
            {
                var dto = _mapper.Map<BoardGameViewModel, BoardGameDto>(boardGame);
                await boardGameService.UpdateBoardGameAsync(dto);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await BoardGameExists(boardGameService, boardGame.BoardGameId)) return NotFound();

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: BoardGames/Delete/5
        public async Task<IActionResult> Delete([FromServices] IBoardGameService boardGameService, int? id)
        {
            if (id == null) return NotFound();

            var boardGame = await boardGameService.GetBoardGameForIdAsync(id.Value);
            if (boardGame == null) return NotFound();

            var viewModel = _mapper.Map<BoardGameDto, BoardGameViewModel>(boardGame.Value);
            return View(viewModel);
        }

        // POST: BoardGames/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed([FromServices] IBoardGameService boardGameService, int id)
        {
            await boardGameService.DeleteBoardGameWithIdAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private static async Task<bool> BoardGameExists(IBoardGameService boardGameService, int id) => await boardGameService.BoardGameExists(id);
    }
}