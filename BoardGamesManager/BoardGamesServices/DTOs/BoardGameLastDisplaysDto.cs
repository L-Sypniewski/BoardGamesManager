using System;

namespace BoardGamesServices.DTOs
{
    public struct BoardGameLastDisplaysDto
    {
        public int BoardGameId { get; set; }
        public DateTimeOffset DisplayDatetime { get; set; }
        public string Source { get; set; }

        public BoardGameLastDisplaysDto(int boardGameId, DateTimeOffset displayDatetime, string source)
        {
            BoardGameId = boardGameId;
            DisplayDatetime = displayDatetime;
            Source = source;
        }

        public override string ToString() => $"BoardGameId: {BoardGameId}, DisplayDatetime: {DisplayDatetime}, Source: {Source}";
    }
}