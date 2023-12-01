using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Dota_Meta_Analyser_version_a._01
{
    class Hero
    {
        public enum Faction 
        {
            Radiant,
            Dire
        }

        public string Name { get; set; }
        public string Role { get; set; }
        public Faction FactionPlayed { get; set; }
        public int TotalGames { get; set; }
        public int TotalGamesRadiant { get; set; }
        public int TotalGamesDire { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int WinsRadiant { get; set; }
        public int LossesRadiant { get; set; }
        public int WinsDire { get; set; }
        public int LossesDire { get; set; }
        public Dictionary<string, int> RoleCounters { get; set; }
        public Dictionary<string, int> RoleTotalGames { get; set; }
        public Dictionary<string, int> RoleWins { get; set; }

        public Hero()
        {
            RoleCounters = new Dictionary<string, int>
            {
                { "Carry", 0 },
                { "Mid", 0 },
                { "Offlane", 0 },
                { "Pos4 Support", 0 },
                { "Pos5 Support", 0 }
            };

                    RoleTotalGames = new Dictionary<string, int>
            {
                { "Carry", 0 },
                { "Mid", 0 },
                { "Offlane", 0 },
                { "Pos4 Support", 0 },
                { "Pos5 Support", 0 }
            };

                    RoleWins = new Dictionary<string, int>
            {
                { "Carry", 0 },
                { "Mid", 0 },
                { "Offlane", 0 },
                { "Pos4 Support", 0 },
                { "Pos5 Support", 0 }
            };

        }

        public double CalculateWinRate()
        {
            if (TotalGames == 0)
                return 0.0;

            return (double)Wins / TotalGames * 100.0;
        }

        public double CalculateWinRateForFaction(Faction faction)
        {
            int totalGamesForFaction = faction == Faction.Radiant ? TotalGamesRadiant : TotalGamesDire;
            int winsForFaction = faction == Faction.Radiant ? WinsRadiant : WinsDire;

            if (totalGamesForFaction == 0)
                return 0.0;

            return (double)winsForFaction / totalGamesForFaction * 100.0;
        }

        public void UpdateRoleStats(string role, bool win)
        {            
            if (RoleTotalGames.ContainsKey(role))
                RoleTotalGames[role]++;
            else
                RoleTotalGames[role] = 1;
            
            if (win)
            {
                if (RoleWins.ContainsKey(role))
                    RoleWins[role]++;
                else
                    RoleWins[role] = 1;
            }
        }

        public double CalculateWinRateForRole(string role)
        {
            int totalGamesForRole = RoleTotalGames.ContainsKey(role) ? RoleTotalGames[role] : 0;
            int winsForRole = RoleWins.ContainsKey(role) ? RoleWins[role] : 0;

            if (totalGamesForRole == 0)
                return 0.0;

            return (double)winsForRole / totalGamesForRole * 100.0;
        }











    }
}
