using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

namespace EfCoreData.Configurations
{
    internal sealed class BoardGameConfiguration : IEntityTypeConfiguration<BoardGame>
    {
        public void Configure(EntityTypeBuilder<BoardGame> builder)
        {
            builder.Property(boardGame =>
                                 boardGame.BoardGameId)
                   .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                   .HasAnnotation("Sqlite:Autoincrement", true);

            builder
                .Property(boardGame => boardGame.Name)
                .HasMaxLength(60);

            builder.HasCheckConstraint("min_age_constraint", $"{nameof(BoardGame.MinRecommendedAge)} >= 3 AND {nameof(BoardGame.MinRecommendedAge)} <= 18");

            builder.HasCheckConstraint("min_players_constraint", $"{nameof(BoardGame.MinPlayers)} >= 1");

            builder.HasCheckConstraint("max_players_constraint", $"{nameof(BoardGame.MaxPlayers)} >= 1");

            builder.HasCheckConstraint("min_players_lesser_or_equal_to_max_player_constraint", 
                $"{nameof(BoardGame.MinPlayers)} <= {nameof(BoardGame.MaxPlayers)}");

            // I assumed there might be some games with the same name therefore I consider games to be equal only if they have all properties equal
            builder
                .HasIndex(nameof(BoardGame.Name), nameof(BoardGame.MaxPlayers),
                          nameof(BoardGame.MinPlayers), nameof(BoardGame.MinRecommendedAge))
                .IsUnique();
        }
    }
}