using System;
using EfCoreData.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EfCoreData.Options
{
    public sealed class BoardGamesDbContextOptionsFactory
    {
        public const string BoardGamesEfCore = "BoardGameEfCore";
        private string ConnectionString { get; }
        private DatabaseType Database { get; }


        public BoardGamesDbContextOptionsFactory(IConfiguration configuration)
        {
            ConnectionString = configuration.GetConnectionString("Default");
            Database = Enum.Parse<DatabaseType>(configuration["Database"]);
        }

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

        private enum DatabaseType
        {
            SqlServer,
            Sqlite
        }
    }
}