namespace Models
{
    public class BoardGame
    {
        public int BoardGameId { get; set; }

        private string _name;
        public string Name
        {
            get => _name;
            set => _name = value.Trim();
        }

        public byte MinPlayers { get; set; }

        public byte MaxPlayers { get; set; }

        public byte MinRecommendedAge { get; set; }

        public BoardGame(string name, byte minPlayers, byte maxPlayers, byte minRecommendedAge)
        {
            Name = name;
            MinPlayers = minPlayers;
            MaxPlayers = maxPlayers;
            MinRecommendedAge = minRecommendedAge;
        }
    }
}