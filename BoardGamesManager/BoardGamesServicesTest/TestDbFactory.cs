using System;
using System.Data.Common;
using EfCoreData.DbContext;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace BoardGamesServicesTest
{
    public class TestDbFactory : IDisposable
    {
        private DbConnection? _sqliteInMemoryConnection;

        public void Dispose() => _sqliteInMemoryConnection?.Dispose();

        public BoardGamesDbContext CreateDbContext()
        {
            _sqliteInMemoryConnection = new SqliteConnection("Filename=:memory:");
            _sqliteInMemoryConnection.Open();

            var dbContextOptions = new DbContextOptionsBuilder<BoardGamesDbContext>()
                                   .UseSqlite(_sqliteInMemoryConnection).Options;

            var dbContext = new BoardGamesDbContext(dbContextOptions);

            dbContext.Database.EnsureCreated();

            return dbContext;
        }
    }
}