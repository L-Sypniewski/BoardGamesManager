namespace Models
{
    public class BoardGame
    {
        public int BoardGameId { get; private set; }

        public string Name { get; }

        public byte MinPlayers { get; }

        public byte MaxPlayers { get; }

        public byte MinRecommendedAge { get; }

        public BoardGame(string name, byte minPlayers, byte maxPlayers, byte minRecommendedAge)
        {
            Name = name.Trim();
            MinPlayers = minPlayers;
            MaxPlayers = maxPlayers;
            MinRecommendedAge = minRecommendedAge;
        }
    }
}