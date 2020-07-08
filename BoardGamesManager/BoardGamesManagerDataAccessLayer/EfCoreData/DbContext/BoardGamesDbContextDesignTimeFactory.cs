using System;
using System.IO;
using EfCoreData.Options;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace EfCoreData.DbContext
{
    public sealed class BoardGamesDbContextDesignTimeFactory : IDesignTimeDbContextFactory<BoardGamesDbContext>
    {
        public BoardGamesDbContext CreateDbContext(string[] args)
        {
            var environmentName = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ?? "Production";

            var configuration = new ConfigurationBuilder()
                                .SetBasePath(Directory.GetCurrentDirectory())
                                .AddJsonFile("appsettings.json")
                                .AddJsonFile($"appsettings.{environmentName}.json", true, true)
                                .Build();

            var boardGamesDbOptions = configuration.GetSection(BoardGamesDbOptions.BoardGamesDb)
                                                   .Get<BoardGamesDbOptions>();

            var dbContextOptions = new BoardGamesDbContextOptionsFactory(boardGamesDbOptions).Create();
            return new BoardGamesDbContext(dbContextOptions);
        }
    }
}