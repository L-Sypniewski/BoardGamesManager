using System;
using EfCoreData.DbContext;
using EfCoreData.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BoardGamesManagerApi.DependencyInjection
{
    public static class AddBoardGameServiceExtension
    {

        //TODO Move to data access layer to remove code duplication
        public static IServiceCollection ConfigureBoardGamesDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var boardGamesDbOptions = configuration.GetSection(BoardGamesDbOptions.BoardGamesDb)
                .Get<BoardGamesDbOptions>();

            switch (boardGamesDbOptions.Database)
            {
                case BoardGamesDbOptions.DatabaseType.SqlServer:
                    services.AddDbContext<BoardGamesDbContext>(options =>
                        options.UseSqlServer(boardGamesDbOptions.ConnectionString));
                    break;
                case BoardGamesDbOptions.DatabaseType.Sqlite:
                    services.AddDbContext<BoardGamesDbContext>(options =>
                        options.UseSqlite(boardGamesDbOptions.ConnectionString));
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            return services;
        }
    }
}