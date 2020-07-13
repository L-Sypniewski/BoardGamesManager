using System;
using System.Collections.Generic;

namespace BoardGamesManagerMvc.Models
{
    public class BoardGameWithLastDisplaysViewModel
    {
        public int BoardGameId { get; set; }

        public string Name { get; set; }

        public byte MinPlayers { get; set; }

        public byte MaxPlayers { get; set; }

        public byte MinRecommendedAge { get; set; }

        public LastDisplayViewModel[] LastDisplays { get; set; }

        public BoardGameWithLastDisplaysViewModel()
        {
        }

        public BoardGameWithLastDisplaysViewModel(int boardGameId, string name, byte minPlayers, byte maxPlayers,
                                                  byte minRecommendedAge, LastDisplayViewModel[] lastDisplays)
        {
            BoardGameId = boardGameId;
            Name = name;
            MinPlayers = minPlayers;
            MaxPlayers = maxPlayers;
            MinRecommendedAge = minRecommendedAge;
            LastDisplays = lastDisplays;
        }
    }
}