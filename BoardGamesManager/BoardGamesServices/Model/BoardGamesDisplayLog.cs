using System;

namespace BoardGamesServices.Model
{
    public struct BoardGamesDisplayLog
    {
        public int BoardGameId { get; set; }
        public DateTimeOffset DisplayDatetime { get; set; }
        public string Source { get; set; }

        public BoardGamesDisplayLog(int boardGameId, DateTimeOffset displayDatetime, string source)
        {
            BoardGameId = boardGameId;
            DisplayDatetime = displayDatetime;
            Source = source;
        }

        public override string ToString() => $"BoardGameId: {BoardGameId}, DisplayDatetime: {DisplayDatetime}, Source: {Source}";
    }
}