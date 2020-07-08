namespace BoardGamesServices.DTOs
{
    public readonly struct BoardGameDto
    {
        public readonly int BoardGameId;
        public readonly string Name;
        public readonly byte MinPlayers;
        public readonly byte MaxPlayers;
        public readonly byte MinRecommendedAge;

        public BoardGameDto(int boardGameId, string name, byte minPlayers, byte maxPlayers, byte minRecommendedAge)
        {
            BoardGameId = boardGameId;
            Name = name;
            MinPlayers = minPlayers;
            MaxPlayers = maxPlayers;
            MinRecommendedAge = minRecommendedAge;
        }

        public override string ToString() => $"BoardGameId: {BoardGameId}, Name: {Name}, MinPlayers: {MinPlayers}," +
                                             $" MaxPlayers: {MaxPlayers}, MinRecommendedAge: {MinRecommendedAge}";
    }
}