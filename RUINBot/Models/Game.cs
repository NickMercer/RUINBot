using System;
using System.Collections.Generic;
using System.Text;

namespace RUINBot.Models
{
    public class Game
    {
        public string Name { get; set; }
        public int MaxPlayers { get; set; }
        public int AverageSessionInMinutes { get; set; }
        public int Weight { get; set; }
        public List<ulong> Players { get; set; }

        public Game(string name, int maxPlayers, int sessionTime, int weight, List<ulong> players)
        {
            Name = name;
            MaxPlayers = maxPlayers;
            AverageSessionInMinutes = sessionTime;
            Weight = weight;
            Players = players;
        }
    }
}
