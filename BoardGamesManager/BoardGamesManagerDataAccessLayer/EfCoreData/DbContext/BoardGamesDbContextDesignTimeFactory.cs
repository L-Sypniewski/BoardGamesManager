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
            var configuration = new ConfigurationBuilder()
                                .SetBasePath(Directory.GetCurrentDirectory())
                                .AddJsonFile("appsettings.json")
                                .Build();

            var options = new BoardGamesDbContextOptionsFactory(configuration).Create();
            return new BoardGamesDbContext(options);
        }
    }
}