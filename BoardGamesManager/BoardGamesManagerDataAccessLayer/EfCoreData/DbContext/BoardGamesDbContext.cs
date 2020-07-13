using EfCoreData.Configurations;
using Microsoft.EntityFrameworkCore;
using Models;

namespace EfCoreData.DbContext
{
    public sealed class BoardGamesDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public BoardGamesDbContext(DbContextOptions<BoardGamesDbContext> options) : base(options)
        {
        }

        public DbSet<BoardGame> BoardGames { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder) => modelBuilder.ApplyConfiguration(new BoardGameConfiguration());
    }
}