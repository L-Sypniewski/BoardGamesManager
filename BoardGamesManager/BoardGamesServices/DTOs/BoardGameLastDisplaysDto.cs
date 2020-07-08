using System;

namespace BoardGamesServices.DTOs
{
    public readonly struct BoardGameLastDisplaysDto
    {
        private readonly int _boardGameId;
        public readonly DateTimeOffset DisplayDatetime;
        public readonly string Source;

        public BoardGameLastDisplaysDto(int boardGameId, DateTimeOffset displayDatetime, string source)
        {
            _boardGameId = boardGameId;
            DisplayDatetime = displayDatetime;
            Source = source;
        }
    }
}