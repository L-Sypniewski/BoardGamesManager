namespace BoardGamesManagerMvc.Models
{
    public class BoardGameViewModel
    {
        public BoardGameViewModel()
        {
        }

        public BoardGameViewModel(int boardGameId, string name, byte minPlayers, byte maxPlayers, byte minRecommendedAge)
        {
            BoardGameId = boardGameId;
            Name = name;
            MinPlayers = minPlayers;
            MaxPlayers = maxPlayers;
            MinRecommendedAge = minRecommendedAge;
        }

        public int BoardGameId { get; set; }

        public string Name { get; set; }

        public byte MinPlayers { get; set; }

        public byte MaxPlayers { get; set; }

        public byte MinRecommendedAge { get; set; }

        public override string ToString() => $"BoardGameId: {BoardGameId}, Name: {Name}, MinPlayers: {MinPlayers}," +
                                             $" MaxPlayers: {MaxPlayers}, MinRecommendedAge: {MinRecommendedAge}";
    }
}