using System;
using EfCoreData.DbContext;
using Microsoft.EntityFrameworkCore;
using static EfCoreData.Options.BoardGamesDbOptions;

namespace EfCoreData.Options
{
    public sealed class BoardGamesDbContextOptionsFactory
    {
        public BoardGamesDbContextOptionsFactory(BoardGamesDbOptions options)
        {
            ConnectionString = options.ConnectionString;
            Database = options.Database;
        }

        private string? ConnectionString { get; }
        private DatabaseType Database { get; }

        public DbContextOptions<BoardGamesDbContext> Create()
        {
            var optionsBuilder = new DbContextOptionsBuilder<BoardGamesDbContext>();

            switch (Database)
            {
                case DatabaseType.SqlServer:
                    optionsBuilder.UseSqlServer(ConnectionString);
                    break;
                case DatabaseType.Sqlite:
                    optionsBuilder.UseSqlite(ConnectionString);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return optionsBuilder.Options;
        }
    }
}