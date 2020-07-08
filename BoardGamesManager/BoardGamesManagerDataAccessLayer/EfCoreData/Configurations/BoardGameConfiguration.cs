using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

namespace EfCoreData.Configurations
{
    internal class BoardGameConfiguration: IEntityTypeConfiguration<BoardGame>
    {
        public void Configure(EntityTypeBuilder<BoardGame> builder)
        {
            builder
                .Property(boardGame => boardGame.Name)
                .HasMaxLength(100);

            // I assumed there might be some games with the same name therefore I consider games to be equal only if they have all properties equal
            builder
                .HasIndex(nameof(BoardGame.Name), nameof(BoardGame.MaxPlayers),
                          nameof(BoardGame.MinPlayers), nameof(BoardGame.MinRecommendedAge))
                .IsUnique();
        }
    }
}