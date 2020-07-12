using System;
using AutoMapper;
using BoardGamesServices.Mapping;
using BoardGamesServices.Services.BoardGame;
using EfCoreData.DbContext;
using EfCoreData.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BoardGamesManagerApi.DependencyInjection
{
    public static class AddBoardGameServiceExtension
    {
        public static IServiceCollection AddBoardGameService(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureBoardGamesDbContext(configuration);
            services.AddAutoMapper(typeof(ApplicationLogMappingProfile), typeof(BoardGamesMappingProfile));
            services.AddScoped<IBoardGameService, DbBoardGameService>();
            return services;
        }

        private static IServiceCollection ConfigureBoardGamesDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var boardGamesDbOptions = configuration.GetSection(BoardGamesDbOptions.BoardGamesDb)
                                                   .Get<BoardGamesDbOptions>();

            switch (boardGamesDbOptions.Database)
            {
                case BoardGamesDbOptions.DatabaseType.SqlServer:
                    services.AddDbContext<BoardGamesDbContext>(options =>
                                                                   options.UseSqlServer(boardGamesDbOptions.ConnectionString));
                    break;
                case  BoardGamesDbOptions.DatabaseType.Sqlite:
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