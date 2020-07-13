namespace EfCoreData.Options
{
    public sealed class BoardGamesDbOptions
    {
        public enum DatabaseType
        {
            SqlServer,
            Sqlite
        }

        public const string BoardGamesDb = "BoardGamesDb";
        public string? ConnectionString { get; set; }
        public DatabaseType Database { get; set; }
    }
}