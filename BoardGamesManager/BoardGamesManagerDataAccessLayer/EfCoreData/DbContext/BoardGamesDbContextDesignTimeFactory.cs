using System;
using System.IO;
using EfCoreData.Options;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace EfCoreData.DbContext
{
    public class BoardGamesDbContextDesignTimeFactory : IDesignTimeDbContextFactory<BoardGamesDbContext>
    {
        public BoardGamesDbContext CreateDbContext(string[] args)
        {
            var environmentName = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ?? "Production";

            var configuration = new ConfigurationBuilder()
                                .SetBasePath(Directory.GetCurrentDirectory())
                                .AddJsonFile("appsettings.json")
                                .AddJsonFile($"appsettings.{environmentName}.json", true, true)
                                .Build();

            var options = new BoardGamesDbContextOptionsFactory(configuration).Create();
            return new BoardGamesDbContext(options);
        }
    }
}