namespace EfCoreData.Options
{
    public sealed class BoardGamesDbOptions
    {
        public const string BoardGamesDb = "BoardGamesDb";
        public string? ConnectionString { get; set; }
        public DatabaseType Database { get; set; }

        public enum DatabaseType
        {
            SqlServer,
            Sqlite
        }
    }
}