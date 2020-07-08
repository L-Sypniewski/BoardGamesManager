using Models;

namespace ModelTest.Validation
{
    internal class BoardGameBuilder
    {
        private string _name = "name";
        private byte _minPlayers = 1;
        private byte _maxPlayers = 1;
        private byte _minRecommendedAge = 3;

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

        public BoardGame Build() => new BoardGame(_name, _minPlayers, _maxPlayers, _minRecommendedAge);
    }
}