namespace BoardGamesServices.DTOs
{
    public readonly struct BoardGameDto
    {
        public int BoardGameId { get; }

        public string Name { get; }

        public byte MinPlayers { get; }

        public byte MaxPlayers { get; }

        public byte MinRecommendedAge { get; }

        public BoardGameDto(int boardGameId, string name, byte minPlayers, byte maxPlayers, byte minRecommendedAge)
        {
            BoardGameId = boardGameId;
            Name = name;
            MinPlayers = minPlayers;
            MaxPlayers = maxPlayers;
            MinRecommendedAge = minRecommendedAge;
        }
    }
}