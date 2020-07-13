using Microsoft.EntityFrameworkCore.Migrations;

namespace EfCoreData.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "BoardGames",
                table => new
                {
                    BoardGameId = table.Column<int>(nullable: false)
                                       .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(maxLength: 60, nullable: false),
                    MinPlayers = table.Column<byte>(nullable: false),
                    MaxPlayers = table.Column<byte>(nullable: false),
                    MinRecommendedAge = table.Column<byte>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoardGames", x => x.BoardGameId);
                    table.CheckConstraint("min_age_constraint", "MinRecommendedAge >= 3 AND MinRecommendedAge <= 18");
                    table.CheckConstraint("min_players_constraint", "MinPlayers >= 1");
                    table.CheckConstraint("max_players_constraint", "MaxPlayers >= 1");
                    table.CheckConstraint("min_players_lesser_or_equal_to_max_player_constraint", "MinPlayers <= MaxPlayers");
                });

            migrationBuilder.CreateIndex(
                "IX_BoardGames_Name_MaxPlayers_MinPlayers_MinRecommendedAge",
                "BoardGames",
                new[] {"Name", "MaxPlayers", "MinPlayers", "MinRecommendedAge"},
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder) => migrationBuilder.DropTable(
            "BoardGames");
    }
}