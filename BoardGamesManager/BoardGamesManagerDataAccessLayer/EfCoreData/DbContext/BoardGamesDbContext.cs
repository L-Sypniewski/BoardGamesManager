using EfCoreData.Configurations;
using Microsoft.EntityFrameworkCore;
using Models;

namespace EfCoreData.DbContext
{
    public class BoardGamesDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbSet<BoardGame> BoardGames { get; set; }

        public BoardGamesDbContext(DbContextOptions<BoardGamesDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BoardGameConfiguration());
        }
    }
}