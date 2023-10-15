using SportTeamManagementConsole.Models;
using System;
using System.Collections.Generic;

namespace SportTeamManagementConsoe
{
    class Program
    {
        static void Main(string[] args)
        {

            Dictionary<int, string> mainMenu = new Dictionary<int, string>();

            mainMenu.Add(0, "Create coach");
            mainMenu.Add(1, "Create player");
            mainMenu.Add(2, "Create team");
            mainMenu.Add(3, "Edit coach");
            mainMenu.Add(4, "Edit player");
            mainMenu.Add(5, "Exit");

            // Init objects
            Coach coach = new Coach();


            foreach (KeyValuePair<int,string> item in mainMenu)
            {
                Console.WriteLine($"{item.Key} : {item.Value}");
            }

            bool converted = Int32.TryParse(Console.ReadLine(), out int userInput);
            if (converted)
            {
                switch (userInput)
                {
                    case 0:
                        Console.WriteLine($"Put in a first name");

                        Console.ReadLine();
                        break;
                    case 1:
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                }
            }

        }
    }
}
