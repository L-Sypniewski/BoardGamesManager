using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BoardGamesServices.DTOs;
using BoardGamesServices.Services.BoardGame;
using EfCoreData.DbContext;
using FluentAssertions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Models;
using Moq;
using Xunit;

namespace BoardGamesServicesTest.Services.BoardGame
{
    public class DbBoardGameServiceTest : IDisposable
    {
        private readonly TestDbFactory _testDbFactory;
        private readonly BoardGamesDbContext _dbContext;
        private readonly Mock<IMapper> _mapperMock = new Mock<IMapper>();
        private readonly DbBoardGameService _sut;

        public DbBoardGameServiceTest()
        {
            _testDbFactory = new TestDbFactory();
            _dbContext = _testDbFactory.CreateDbContext();

            _sut = new DbBoardGameService(_dbContext, _mapperMock.Object);
        }

        [Fact(DisplayName = "All BoardGames from a database are returned after being mapped when service gets all board games")]
        public void All_BoardGames_from_a_database_are_returned_when_after_being_mapped_service_gets_all_board_games()
        {
            _mapperMock.Setup(mock => mock.Map<BoardGameDto>(It.IsAny<Models.BoardGame>()))
                       .Returns<Models.BoardGame>(boardGame => DtoWithName(boardGame.Name));

            // _dbContext.BoardGames.AddRange(TestBoardGames); //TODO this line causes stackoverflow
            var s = _dbContext.BoardGames.ToArray();

            var actualBoardGames = _sut.GetAllBoardGames().ToArrayAsync().Result;

            var expectedBoardGames = new[]
            {
                DtoWithName("Name1"),
                DtoWithName("Name2"),
                DtoWithName("Name3"),
                DtoWithName("Name4"),
            };

            actualBoardGames.Should().BeEquivalentTo(expectedBoardGames);
        }

        private static Models.BoardGame[] TestBoardGames => new[]
        {
            new BoardGameBuilder().WithName("Name1").WithId(1).Build(),
            new BoardGameBuilder().WithName("Name2").WithId(4).Build(),
            new BoardGameBuilder().WithName("Name3").WithId(3).Build(),
            new BoardGameBuilder().WithName("Name4").WithId(2).Build()
        };

        private static BoardGameDto DtoWithName(string name) => new BoardGameDto(0, name, 0, 0, 0);

        public void Dispose() => _testDbFactory.Dispose();
    }
}