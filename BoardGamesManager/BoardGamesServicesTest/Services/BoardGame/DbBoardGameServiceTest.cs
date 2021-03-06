using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BoardGamesServices.DTOs;
using BoardGamesServices.Services.BoardGame;
using EfCoreData.DbContext;
using FluentAssertions;
using Models;
using Moq;
using Xunit;

namespace BoardGamesServicesTest.Services.BoardGame
{
    public class DbBoardGameServiceTest : IDisposable
    {
        public DbBoardGameServiceTest()
        {
            _testDbFactory = new TestDbFactory();
            _dbContext = _testDbFactory.CreateDbContext();

            _sut = new DbBoardGameService(_dbContext, _mapperMock.Object);
        }

        public void Dispose() => _testDbFactory.Dispose();
        private readonly TestDbFactory _testDbFactory;
        private readonly BoardGamesDbContext _dbContext;
        private readonly Mock<IMapper> _mapperMock = new Mock<IMapper>();
        private readonly DbBoardGameService _sut;

        private static Models.BoardGame[] TestBoardGames => new[]
        {
            new BoardGameBuilder().WithName("Name1").WithId(1).Build(),
            new BoardGameBuilder().WithName("Name2").WithId(4).Build(),
            new BoardGameBuilder().WithName("Name3").WithId(3).Build(),
            new BoardGameBuilder().WithName("Name4").WithId(2).Build()
        };

        private static BoardGameDto DtoWithName(string name) => new BoardGameDto(0, name, 0, 0, 0);


        // The test might be split into two:
        // The first: check if values from DB are passed to mapper by calling Verify() from Moq framework
        // The second: check if values from mapper are returned
        [Fact(DisplayName = "All BoardGames from a database are returned after being mapped when service gets all board games")]
        public async Task All_BoardGames_from_a_database_are_returned_when_after_being_mapped_service_gets_all_board_games()
        {
            _mapperMock.Setup(mock => mock.Map<BoardGameDto>(It.IsAny<Models.BoardGame>()))
                       .Returns<Models.BoardGame>(boardGame => DtoWithName(boardGame.Name));

            await _dbContext.BoardGames.AddRangeAsync(TestBoardGames); //This line causes stackoverflow on Ubuntu 20.10, but it works on the latest MacOS
            await _dbContext.SaveChangesAsync();

            var actualBoardGames = await _sut.GetBoardGamesAsync().ToArrayAsync();

            var expectedBoardGames = new[]
            {
                DtoWithName("Name1"),
                DtoWithName("Name2"),
                DtoWithName("Name3"),
                DtoWithName("Name4")
            };

            actualBoardGames.Should().BeEquivalentTo(expectedBoardGames);
        }
    }
}