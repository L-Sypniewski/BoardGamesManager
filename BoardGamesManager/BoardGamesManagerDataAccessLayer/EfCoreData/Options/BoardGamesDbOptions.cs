namespace EfCoreData.Options
{
    public sealed class BoardGamesDbOptions
    {
        public const string BoardGamesDb = "BoardGamesDb";
        public string ConnectionString { get; set; }
        public string Database { get; set; }
    }
}