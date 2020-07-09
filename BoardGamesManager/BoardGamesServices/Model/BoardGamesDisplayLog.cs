using System;

namespace BoardGamesServices.Model
{
    public readonly struct BoardGamesDisplayLog
    {
        public readonly int BoardGameId;
        public readonly DateTimeOffset DateTimeOffset;
        public readonly string Source;

        public BoardGamesDisplayLog(int boardGameId, DateTimeOffset dateTimeOffset, string source)
        {
            BoardGameId = boardGameId;
            DateTimeOffset = dateTimeOffset;
            Source = source;
        }
    }
}