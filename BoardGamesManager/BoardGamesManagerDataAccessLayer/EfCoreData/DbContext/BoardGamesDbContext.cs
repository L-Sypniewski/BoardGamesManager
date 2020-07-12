using EfCoreData.Configurations;
using Microsoft.EntityFrameworkCore;
using Models;

namespace EfCoreData.DbContext
{
    public sealed class BoardGamesDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbSet<BoardGame> BoardGames { get; set; } = null!;

        public BoardGamesDbContext(DbContextOptions<BoardGamesDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BoardGameConfiguration());
        }
    }
}