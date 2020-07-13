namespace Models
{
    public class BoardGame
    {
        private string _name = null!;

        public BoardGame(string name, byte minPlayers, byte maxPlayers, byte minRecommendedAge)
        {
            Name = name;
            MinPlayers = minPlayers;
            MaxPlayers = maxPlayers;
            MinRecommendedAge = minRecommendedAge;
        }

        public int BoardGameId { get; set; }

        public string Name
        {
            get => _name;
            set => _name = value.Trim();
        }

        public byte MinPlayers { get; set; }

        public byte MaxPlayers { get; set; }

        public byte MinRecommendedAge { get; set; }
    }
}