using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dota_Meta_Analyser_version_a._01
{
    class InputHandler
    {
        private DotaDatabase database;

        public InputHandler(DotaDatabase db)
        {
            database = db;
        }

        public void GetUserInput()
        {
            while (true)
            {
                Console.Write("Enter hero name (or 'exit' to quit): ");
                string enteredHeroName = Console.ReadLine();

                if (enteredHeroName.ToLower() == "exit")
                    break;

                string heroRole;
                while (true)
                {
                    Console.Write("Enter hero role (Carry/Mid/Offlane/Pos4 Support/Pos5 Support): ");
                    heroRole = Console.ReadLine().ToLower();

                    if (heroRole == "carry" || heroRole == "mid" || heroRole == "offlane" ||
                        heroRole == "pos4 support" || heroRole == "pos5 support")
                    {
                        if (heroRole == "pos4 support")
                            heroRole = "Pos4 Support";
                        else if (heroRole == "pos5 support")
                            heroRole = "Pos5 Support";

                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid role. Please enter Carry, Mid, Offlane, Pos4 Support, or Pos5 Support.");
                    }
                }

                Hero.Faction heroFaction;
                while (true)
                {
                    Console.Write("Enter faction (Radiant or Dire): ");
                    string factionInput = Console.ReadLine().ToLower();
                    if (factionInput == "radiant")
                    {
                        heroFaction = Hero.Faction.Radiant;
                        break;
                    }
                    else if (factionInput == "dire")
                    {
                        heroFaction = Hero.Faction.Dire;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid faction. Please enter Radiant or Dire.");
                    }
                }

                Console.Write("Did the hero win the game? (Win/Lose): ");
                string winOrLose = Console.ReadLine();
                bool didWin = winOrLose.ToLower() == "win";

                database.AddHero(enteredHeroName, heroRole, heroFaction);
                database.AddGameResult(enteredHeroName, heroFaction, didWin, heroRole);
                
                Console.WriteLine("Continuing to the next hero...");
            }
        }

        public List<string> GetSelectedHeroNames()
        {
            List<string> selectedHeroNames = new List<string>();
            Console.Write("Enter hero names to display (separated by commas): ");
            string input = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(input))
            {
                string[] heroNames = input.Split(',');
                foreach (var heroName in heroNames)
                {
                    string trimmedHeroName = heroName.Trim();
                    selectedHeroNames.Add(trimmedHeroName);
                }
            }

            return selectedHeroNames;
        }

        public void DisplayAllHeroNames(List<string> heroNames)
        {
            Console.WriteLine("\nAvailable hero data:");
            foreach (var name in heroNames)
            {
                Console.WriteLine(name);
            }
        }
    }
}
