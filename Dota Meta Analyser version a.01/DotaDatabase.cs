using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static Dota_Meta_Analyser_version_a._01.Hero;

namespace Dota_Meta_Analyser_version_a._01
{
    class DotaDatabase
    {
        private Dictionary<string, Hero> heroesDatabase = new Dictionary<string, Hero>();
        private Dictionary<Hero.Faction, int> factionGames = new Dictionary<Hero.Faction, int>
        {
            { Hero.Faction.Radiant, 0},
            { Hero.Faction.Dire, 0}
        };

        public void AddHero(string name, string role, Hero.Faction faction)
        {
            if (!heroesDatabase.ContainsKey(name))
            {
                heroesDatabase[name] = new Hero
                {
                    Name = name,
                    Role = role,
                    FactionPlayed = faction,
                    TotalGames = 0,
                    TotalGamesRadiant = 0,
                    TotalGamesDire = 0
                };
            }
        }

        public void AddGameResult(string heroName, Hero.Faction faction, bool win, string role)
        {
            if (heroesDatabase.ContainsKey(heroName))
            {
                
                if (faction == Hero.Faction.Radiant)
                {
                    factionGames[Hero.Faction.Radiant]++;
                    heroesDatabase[heroName].TotalGamesRadiant++;
                }
                else if (faction == Hero.Faction.Dire)
                {
                    factionGames[Hero.Faction.Dire]++;
                    heroesDatabase[heroName].TotalGamesDire++;
                }

                heroesDatabase[heroName].TotalGames++;

                if (win)
                {
                    heroesDatabase[heroName].Wins++;
                    if (faction == Hero.Faction.Radiant)
                        heroesDatabase[heroName].WinsRadiant++;
                    else if (faction == Hero.Faction.Dire)
                        heroesDatabase[heroName].WinsDire++;
                }
                else
                {
                    heroesDatabase[heroName].Losses++;
                    if (faction == Hero.Faction.Radiant)
                        heroesDatabase[heroName].LossesRadiant++;
                    else if (faction == Hero.Faction.Dire)
                        heroesDatabase[heroName].LossesDire++;
                }

                
                string lowercaseRole = role.ToLower();

                
                if (heroesDatabase[heroName].RoleCounters.ContainsKey(lowercaseRole))
                    heroesDatabase[heroName].RoleCounters[lowercaseRole]++;
                else
                    heroesDatabase[heroName].RoleCounters[lowercaseRole] = 1;

                
                heroesDatabase[heroName].UpdateRoleStats(lowercaseRole, win);
            }
        }

        private string FormatRole(string role)
        {
            switch (role.ToLower())
            {
                case "carry":
                    return "Carry";
                case "mid":
                    return "Mid";
                case "offlane":
                    return "Offlane";
                case "pos4 support":
                    return "Pos4 Support";
                case "pos5 support":
                    return "Pos5 Support";
                default:
                    return role;
            }
        }

        public void DisplayHeroStats(string heroName)
        {
            if (heroesDatabase.ContainsKey(heroName))
            {
                var hero = heroesDatabase[heroName];
                Console.WriteLine("Hero Name: " + hero.Name);                
                Console.WriteLine("Total Games: " + hero.TotalGames);
                Console.WriteLine("Wins: " + hero.Wins);
                Console.WriteLine("Losses: " + hero.Losses);
                Console.WriteLine("Win Rate: " + hero.CalculateWinRate().ToString("0.00") + "%");
                Console.WriteLine("\nTotal Games as Radiant: " + factionGames[Hero.Faction.Radiant]);
                Console.WriteLine("Wins as Radiant: " + hero.WinsRadiant);
                Console.WriteLine("Losses as Radiant: " + hero.LossesRadiant);
                Console.WriteLine("Win Rate as Radiant: " + hero.CalculateWinRateForFaction(Faction.Radiant).ToString("0.00") + "%");
                Console.WriteLine("\nTotal Games as Dire: " + factionGames[Hero.Faction.Dire]);
                Console.WriteLine("Wins as Dire: " + hero.WinsDire);
                Console.WriteLine("Losses as Dire: " + hero.LossesDire);
                Console.WriteLine("Win Rate as Dire: " + hero.CalculateWinRateForFaction(Faction.Dire).ToString("0.00") + "%");


                Console.WriteLine("\nWin Rate for Each Role:");
                foreach (var role in hero.RoleCounters)
                {
                    double winRateForRole = hero.CalculateWinRateForRole(role.Key);
                    Console.WriteLine($"{FormatRole(role.Key)}: {winRateForRole.ToString("0.00")}%");
                }
            }
            else
            {
                Console.WriteLine("Hero not found in database.");
            }
        }

        public List<string> GetHeroNames()
        {
            return new List<string>(heroesDatabase.Keys);
        }

        public void SaveData(string filePath)
        {
            
            var dataContainer = new { HeroesDatabase = heroesDatabase, FactionGames = factionGames };

            
            string jsonData = JsonSerializer.Serialize(dataContainer);

            
            File.WriteAllText(filePath, jsonData);
        }

        public void LoadData(string filePath)
        {
            if (File.Exists(filePath))
            {                
                string jsonData = File.ReadAllText(filePath);
                
                var dataContainer = JsonSerializer.Deserialize<DataContainer>(jsonData);
                
                heroesDatabase = dataContainer.HeroesDatabase;
                factionGames = dataContainer.FactionGames;
            }
        }

        private class DataContainer
        {
            public Dictionary<string, Hero> HeroesDatabase { get; set; }
            public Dictionary<Hero.Faction, int> FactionGames { get; set; }
        }
    }
}
