namespace BoardGamesServices.DTOs
{
    public struct BoardGameDto
    {
        public int BoardGameId { get; set; }

        public string Name { get; set; }

        public byte MinPlayers { get; set; }

        public byte MaxPlayers { get; set; }

        public byte MinRecommendedAge { get; set; }

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