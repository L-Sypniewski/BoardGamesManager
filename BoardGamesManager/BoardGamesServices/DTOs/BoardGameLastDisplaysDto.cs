using System;

namespace BoardGamesServices.DTOs
{
    public readonly struct BoardGameLastDisplaysDto
    {
        public DateTimeOffset DisplayDatetime { get; }
        public string Source { get; }

        public BoardGameLastDisplaysDto(DateTimeOffset displayDatetime, string source)
        {
            DisplayDatetime = displayDatetime;
            Source = source;
        }
    }
}