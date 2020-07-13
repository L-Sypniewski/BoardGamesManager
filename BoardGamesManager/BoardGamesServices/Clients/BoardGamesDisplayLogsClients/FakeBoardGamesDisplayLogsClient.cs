using System;
using System.Collections.Generic;
using BoardGamesServices.Model;

namespace BoardGamesServices.Clients.BoardGamesDisplayLogsClients
{
    /* I didn't manage meet the deadline implement a proper mechanism for recording views of each game
        The idea was to use ElasticSearch sink for Serilog with custom field "Application" and use a REST
        service to obtain the data when displaying board game details. This way I wouldn't have to implement
        and maintain a mechanism for recording view, I'd get that 'for free' while simply logging application events
    */
    public class FakeBoardGamesDisplayLogsClient : IBoardGamesDisplayLogsClient
    {
        public async IAsyncEnumerable<BoardGamesDisplayLog> BoardGamesDisplayLogsFor(int boardGameId)
        {
            var random = new Random();
            var numberOfRecords = random.Next(1, 20);

            for (var i = 0; i < numberOfRecords; i++)
            {
                var timestamp = DateTimeOffset
                                .Now
                                .AddDays(random.Next(-3, 3))
                                .AddMinutes(random.Next(-20, 20))
                                .AddSeconds(random.Next(-100, 100))
                                .AddMilliseconds(random.Next(-200, 200));
                yield return new BoardGamesDisplayLog(boardGameId, timestamp, GetApplicationName());
            }
        }

        private static string GetApplicationName()
        {
            var applicationsNames = new[] {"MVC application", "WebAPI"};
            var random = new Random();
            var randomIndex = random.Next(applicationsNames.Length);
            return applicationsNames[randomIndex];
        }
    }
}