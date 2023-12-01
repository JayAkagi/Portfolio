using System.Collections;

namespace Dota_Meta_Analyser_version_a._01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DotaDatabase database = new DotaDatabase();
            database.LoadData("data.json");
            InputHandler inputHandler = new InputHandler(database);

            while (true)
            {  
                inputHandler.GetUserInput();

                Console.Write("Do you want to enter more hero data? (yes/no): ");
                string response = Console.ReadLine().Trim().ToLower();

                if (response == "no")
                {                    
                    break;
                }
                else if (response != "yes")
                {
                    Console.WriteLine("Invalid response. Exiting the program.");
                    break;
                }
            }
            
            List<string> heroNames = database.GetHeroNames();
            inputHandler.DisplayAllHeroNames(heroNames);

            
            List<string> selectedHeroNames = inputHandler.GetSelectedHeroNames();
            Console.WriteLine("\nGame records for selected heroes:");
            foreach (var heroName in selectedHeroNames)
            {
                database.DisplayHeroStats(heroName);
                Console.WriteLine();
            }

            database.SaveData("data.json");
            Console.ReadKey();
        }           
    }
}