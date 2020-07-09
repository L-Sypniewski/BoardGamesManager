namespace Models
{
    public class BoardGameBuilder
    {
        private string _name = "name";
        private byte _minPlayers = 1;
        private byte _maxPlayers = 1;
        private byte _minRecommendedAge = 3;
        private int _id = 0;

        public BoardGameBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public BoardGameBuilder WithMinPlayers(byte minPlayers)
        {
            _minPlayers = minPlayers;
            return this;
        }

        public BoardGameBuilder WithMinRecommendedAge(byte minRecommendedAge)
        {
            _minRecommendedAge = minRecommendedAge;
            return this;
        }

        public BoardGameBuilder WithMaxPlayers(byte maxPlayers)
        {
            _maxPlayers = maxPlayers;
            return this;
        }

        public BoardGameBuilder WithId(int id)
        {
            _id = id;
            return this;
        }

        public BoardGame Build()
        {
            var game = new BoardGame(_name, _minPlayers, _maxPlayers, _minRecommendedAge);
            game.BoardGameId = _id;
            return game;
        }
    }
}