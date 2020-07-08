using EfCoreData.Configurations;
using Microsoft.EntityFrameworkCore;

namespace EfCoreData.DbContext
{
    public class BoardGamesDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public BoardGamesDbContext(DbContextOptions<BoardGamesDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BoardGameConfiguration());
        }
    }
}