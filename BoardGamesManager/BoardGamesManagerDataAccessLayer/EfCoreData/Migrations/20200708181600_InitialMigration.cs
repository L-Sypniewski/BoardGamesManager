﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace EfCoreData.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BoardGames",
                columns: table => new
                {
                    BoardGameId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
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
                });

            migrationBuilder.CreateIndex(
                name: "IX_BoardGames_Name_MaxPlayers_MinPlayers_MinRecommendedAge",
                table: "BoardGames",
                columns: new[] { "Name", "MaxPlayers", "MinPlayers", "MinRecommendedAge" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BoardGames");
        }
    }
}
