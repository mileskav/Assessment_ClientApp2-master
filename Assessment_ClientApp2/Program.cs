using System;
using System.Collections.Generic;
using System.IO;

namespace Assessment_ClientApp2
{

    // **************************************************
    //
    // Assessment: Client App 2.0
    // Author: Kavanagh, Miles
    // Dated: 11/29/2019
    // Level (Novice, Apprentice, or Master): Master
    //
    // **************************************************    

    class Program
    {
        /// <summary>
        /// Main method - app starts here
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.SetWindowSize(140, 30);
           
            //
            // initialize monster list from method
            //
            List<Monster> monsters = ReadFromDataFile();

            //
            // read monsters from data file
            //
            //List<Monster> monsters = ReadFromDataFile();

            //
            // application flow
            //
            DisplayWelcomeScreen();
            DisplayMenuScreen(monsters);
            DisplayClosingScreen();
        }

        #region UTILITY METHODS

        /// <summary>
        /// initialize a list of monsters
        /// </summary>
        /// <returns>list of monsters</returns>
        static List<Monster> InitializeMonsterList()
        {
            //
            // create a list of monsters
            // note: list and object initializers used
            //
            List<Monster> monsters = new List<Monster>()
            {

                new Monster()
                {
                    Name = "Sid",
                    Age = 145,
                    Attitude = Monster.EmotionalState.happy,
                    MonsterTribe = Monster.Tribe.lunar,
                    Active = true
                },

                new Monster()
                {
                    Name = "Lucy",
                    Age = 125,
                    Attitude = Monster.EmotionalState.bored,
                    MonsterTribe = Monster.Tribe.polar,
                    Active = true
                },

                new Monster()
                {
                    Name = "Bill",
                    Age = 934,
                    Attitude = Monster.EmotionalState.sad,
                    MonsterTribe = Monster.Tribe.tropic,
                    Active = true
                },
                
                new Monster()
                {
                    Name = "Boo Boo",
                    Age = 300,
                    Attitude = Monster.EmotionalState.angry,
                    MonsterTribe = Monster.Tribe.lunar,
                    Active = true
                },

                new Monster()
                {
                    Name = "Peach",
                    Age = 98,
                    Attitude = Monster.EmotionalState.happy,
                    MonsterTribe = Monster.Tribe.solar,
                    Active = true
                }

            };

            return monsters;
        }

        #endregion

        #region SCREEN DISPLAY METHODS

        /// <summary>
        /// SCREEN: display and process menu options
        /// </summary>
        /// <param name="monsters">list of monsters</param>
        static void DisplayMenuScreen(List<Monster> monsters)
        {
            bool quitApplication = false;
            string menuChoice;

            do
            {
                DisplayScreenHeader("Main Menu");

                //
                // get user menu choice
                //
                Console.WriteLine("\ta) List All Monsters");
                Console.WriteLine("\tb) View Monster Detail");
                Console.WriteLine("\tc) Add Monster");
                Console.WriteLine("\td) Delete Monster");
                Console.WriteLine("\te) Update Monster");
                Console.WriteLine("\tf) Filter By Attitude");
                Console.WriteLine("\tg) Save Data");
                Console.WriteLine("\tq) Quit");
                Console.Write("\t\tEnter Choice:");
                menuChoice = Console.ReadLine().ToLower();

                //
                // process user menu choice
                //
                switch (menuChoice)
                {
                    case "a":
                        DisplayAllMonsters(monsters);
                        break;

                    case "b":
                        DisplayViewMonsterDetail(monsters);
                        break;

                    case "c":
                        DisplayAddMonster(monsters);
                        break;

                    case "d":
                        DisplayDeleteMonster(monsters);
                        break;

                    case "e":
                        DisplayUpdateMonster(monsters);
                        break;

                    case "f":
                        DisplayFilterByAttitude(monsters);
                        break;

                    case "g":
                        DisplayWriteToDataFile(monsters);
                        break;
                        
                    case "q":
                        quitApplication = true;
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("Please enter a letter for the menu choice.");
                        DisplayContinuePrompt();
                        break;
                }
            } while (!quitApplication);
        }

        /// <summary>
        /// SCREEN: list all monsters
        /// </summary>
        /// <param name="monsters">list of monsters</param>
        static void DisplayAllMonsters(List<Monster> monsters)
        {
            DisplayScreenHeader("All Monsters");

            //
            // DISPLAY table headers
            Console.WriteLine();
            Console.WriteLine(
                "Name".PadRight(15) +
                "Age".PadRight(15) +
                "Birthdate".PadRight(15) +
                "Attitude".PadRight(15) +
                "Tribe".PadRight(12) +
                "Active".PadRight(10) +
                "Greeting".PadRight(15)
                );
            Console.WriteLine(
                "-----".PadRight(15) +
                "-----".PadRight(15) +
                "-----".PadRight(15) +
                "-----".PadRight(15) +
                "------".PadRight(12) +
                "--------".PadRight(10) +
                "-------".PadRight(20)
                );
            foreach (Monster monster in monsters)
            {
                Console.WriteLine(
                monster.Name.ToString().PadRight(15) +
                monster.Age.ToString().PadRight(15) +
                monster.Birthdate.ToShortDateString().PadRight(15) +
                monster.Attitude.ToString().PadRight(15) +
                monster.MonsterTribe.ToString().PadRight(15) +
                monster.Active.ToString().PadRight(8) +
                monster.Greeting().ToString().PadRight(20)
                ) ;
            }

            DisplayContinuePrompt();
        }

        /// <summary>
        /// SCREEN: monster detail
        /// </summary>
        /// <param name="monsters">list of monsters</param>
        static void DisplayViewMonsterDetail(List<Monster> monsters)
        {
            DisplayScreenHeader("Monster Detail");

            //
            // display all monster names
            //
            Console.WriteLine("\tMonster Names");
            Console.WriteLine("\t-------------");
            foreach (Monster monster in monsters)
            {
                Console.WriteLine("\t" + monster.Name);
            }

            //
            // get user monster choice
            //
            Console.WriteLine();
            Console.Write("\tEnter name:");
            string monsterName = Console.ReadLine();

            //
            // get monster object
            //
            Monster selectedMonster = null;
            foreach (Monster monster in monsters)
            {
                if (monster.Name == monsterName)
                {
                    selectedMonster = monster;
                    break;
                }
            }

            //
            // display monster detail
            //
            DisplayScreenHeader($"{selectedMonster.Name} Details");
            MonsterInfo(selectedMonster);

            DisplayContinuePrompt();
        }

        /// <summary>
        /// SCREEN: add a monster
        /// </summary>
        /// <param name="monsters">list of monsters</param>
        static void DisplayAddMonster(List<Monster> monsters)
        {
            Monster newMonster = new Monster();

            DisplayScreenHeader("Add Monster");

            //
            // add monster object property values
            //
            Console.Write("\tName: ");
            newMonster.Name = Console.ReadLine();
            if (String.IsNullOrEmpty(newMonster.Name))
            {
                Console.WriteLine("\tYou have not entered a valid name. Please enter a name of at least one letter.");
                Console.WriteLine();
                Console.Write("\tName: ");
                newMonster.Name = Console.ReadLine();
            }

            Console.Write("\tAge: ");
            if (!int.TryParse(Console.ReadLine(), out int age))
            {
                Console.WriteLine("\tThis is not a valid age. Please enter a numerical value.");
                Console.WriteLine();
                Console.Write("\tAge: ");
                int.TryParse(Console.ReadLine(), out age);
                newMonster.Age = age;
            }
            else
            {
                newMonster.Age = age;
            }

            Console.Write("\tBirthdate[MM/DD/YYYY]: ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime birthdate))
            {
                Console.WriteLine("\tThis is not a valid birthdate. Please enter a numerical value in the MM/DD/YYYY format.");
                Console.WriteLine();
                Console.Write("\tBirthdate: ");
                DateTime.TryParse(Console.ReadLine(), out birthdate);
                newMonster.Birthdate = birthdate;
            }
            else
            {
                newMonster.Birthdate = birthdate;
            }
         
            Console.Write("\tAttitude: ");
            if (!Enum.TryParse(Console.ReadLine(), out Monster.EmotionalState attitude))
            {
                Console.WriteLine("\tThis is not a valid attitude. Please enter 'sad', 'happy', 'angry', or 'bored'.");
                Console.WriteLine();
                Console.Write("\tAttitude: ");
                Enum.TryParse(Console.ReadLine(), out attitude);
                newMonster.Attitude = attitude;
            }
            else
            {
                newMonster.Attitude = attitude;
            }

            Console.Write("\tTribe: ");
            if (!Enum.TryParse(Console.ReadLine(), out Monster.Tribe tribe))
            {
                Console.WriteLine("\tThis is not a valid tribe. Please enter 'lunar', 'solar', 'lunar', or 'tropic'.");
                Console.WriteLine();
                Console.Write("\tTribe: ");
                Enum.TryParse(Console.ReadLine(), out tribe);
                newMonster.MonsterTribe = tribe;
            }
            else
            {
                newMonster.MonsterTribe = tribe;
            }
            

            Console.Write("\tActive? [true/false]: ");
            if (!bool.TryParse(Console.ReadLine(), out bool active))
            {
                Console.WriteLine("\tThis is not a valid answer. Please enter 'true' or 'false'.");
                Console.WriteLine();
                Console.Write("\tActive?: ");
                bool.TryParse(Console.ReadLine(), out active);
                newMonster.Active = active;
            }
            else
            {
                newMonster.Active = active;
            }

            //
            // echo new monster properties
            //
            Console.WriteLine();
            Console.WriteLine("\tNew Monster's Properties");
            Console.WriteLine("\t-------------");
            MonsterInfo(newMonster);
            Console.WriteLine();
            Console.WriteLine("\t-------------");

            DisplayContinuePrompt();

            monsters.Add(newMonster);
        }

        /// <summary>
        /// SCREEN: delete monster
        /// </summary>
        /// <param name="monsters">list of monsters</param>
        static void DisplayDeleteMonster(List<Monster> monsters)
        {
            DisplayScreenHeader("Delete Monster");

            //
            // display all monster names
            //
            Console.WriteLine("\tMonster Names");
            Console.WriteLine("\t-------------");
            foreach (Monster monster in monsters)
            {
                Console.WriteLine("\t" + monster.Name);
            }

            //
            // get user monster choice
            //
            Console.WriteLine();
            Console.Write("\tEnter name:");
            string monsterName = Console.ReadLine();

            //
            // get monster object
            //
            Monster selectedMonster = null;
            foreach (Monster monster in monsters)
            {
                if (monster.Name == monsterName)
                {
                    selectedMonster = monster;
                    break;
                }
            }

            //
            // delete monster
            //
            if (selectedMonster != null)
            {
                selectedMonster.Active = false;
                Console.WriteLine();
                Console.WriteLine($"\t{selectedMonster.Name} is now inactive");
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine($"\t{monsterName} not found");
            }

            DisplayContinuePrompt();
        }

        /// <summary>
        /// SCREEN: update monster
        /// </summary>
        /// <param name="monsters">list of monsters</param>
        /// 
        static void DisplayUpdateMonster(List<Monster> monsters)
        {
            bool validResponse = false;
            Monster selectedMonster = null;

            do
            {
                DisplayScreenHeader("Update Monster");

                //
                // display all monster names
                //
                Console.WriteLine("\tMonster Names");
                Console.WriteLine("\t-------------");
                foreach (Monster monster in monsters)
                {
                    Console.WriteLine("\t" + monster.Name);
                }

                //
                // get user monster choice
                //
                Console.WriteLine();
                Console.Write("\tEnter name:");
                string monsterName = Console.ReadLine();

                //
                // get monster object
                //

                foreach (Monster monster in monsters)
                {
                    if (monster.Name == monsterName)
                    {
                        selectedMonster = monster;
                        validResponse = true;
                        break;
                    }
                }

                //
                // feedback for wrong name choice
                //
                if (!validResponse)
                {
                    Console.WriteLine("\tPlease select a correct name.");
                    DisplayContinuePrompt();
                }

                //
                // update monster
                //

            } while (!validResponse);


            //
            // update monster properties
            //
            string userResponse;
            Console.WriteLine();
            Console.WriteLine("\tReady to update. Press the Enter Key to keep the current info.");
            Console.WriteLine();
            Console.Write($"\tCurrent Name: {selectedMonster.Name} New Name: ");
            userResponse = Console.ReadLine();
            if (userResponse != "")
            {
                selectedMonster.Name = userResponse;
            }

            Console.Write($"\tCurrent Age: {selectedMonster.Age} New Age: ");
            userResponse = Console.ReadLine();
            if (userResponse != "")
            {
                if (!int.TryParse(userResponse, out int age))
                {
                    Console.WriteLine("\tThis is not a valid age. Please enter a numerical value.");
                    Console.WriteLine();
                    Console.Write("\tAge: ");
                    int.TryParse(userResponse, out age);
                    selectedMonster.Age = age;
                } 
            }

            Console.Write($"\tCurrent Birthdate: {selectedMonster.Birthdate:d} New Birthdate: ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime birthdate))
            {
                Console.WriteLine("\tThis is not a valid birthdate. Please enter a numerical value in the MM/DD/YYYY format.");
                Console.WriteLine();
                Console.Write("\tBirthdate: ");
                DateTime.TryParse(Console.ReadLine(), out birthdate);
                selectedMonster.Birthdate = birthdate;
            }

            Console.Write($"\tCurrent Attitude: {selectedMonster.Attitude} New Attitude: ");
            userResponse = Console.ReadLine();
            if (userResponse != "")
            {
                if (!Enum.TryParse(userResponse, out Monster.EmotionalState attitude))
                {
                    Console.WriteLine("\tThis is not a valid attitude. Please enter 'sad', 'happy', 'angry', or 'bored'.");
                    Console.WriteLine();
                    Console.Write("\tAttitude: ");
                    Enum.TryParse(userResponse, out attitude);
                    selectedMonster.Attitude = attitude;
                }
            }

            Console.Write($"\tCurrent Tribe: {selectedMonster.MonsterTribe} New Tribe: ");
            userResponse = Console.ReadLine();
            if (userResponse != "")
            {
                if (!Enum.TryParse(userResponse, out Monster.Tribe tribe))
                {
                    Console.WriteLine("\tThis is not a valid tribe. Please enter 'polar', 'solar', 'lunar', or 'tropic'.");
                    Console.WriteLine();
                    Console.Write("\tTribe: ");
                    Enum.TryParse(userResponse, out tribe);
                    selectedMonster.MonsterTribe = tribe;
                }
            }

            Console.Write($"\tCurrent Active Status: {selectedMonster.Active} New Active Status: ");
            userResponse = Console.ReadLine();
            if (userResponse != "")
            {
                if (!bool.TryParse(userResponse, out bool active))
                {
                    Console.WriteLine("\tThis is not a valid response. Please enter 'true' or 'false'.");
                    Console.WriteLine();
                    Console.Write("\tActive?");
                    bool.TryParse(userResponse, out active);
                    selectedMonster.Active = active;
                }
            }

            //
            // echo updated monster properties
            //
            Console.WriteLine();
            Console.WriteLine("\tNew Monster's Properties");
            Console.WriteLine("\t-------------");
            MonsterInfo(selectedMonster);
            Console.WriteLine();
            Console.WriteLine("\t-------------");

            DisplayContinuePrompt();
        }

        /// <summary>
        /// SCREEN: filter monsters
        /// </summary>
        static void DisplayFilterByAttitude(List<Monster> monsters)
        {
            List<Monster> filteredMonsters = new List<Monster>();
            Monster.EmotionalState selectedAttitude;

            DisplayScreenHeader("Filter by Attitude");
            Console.WriteLine("Enter attitude: ");
            if (!Enum.TryParse(Console.ReadLine(), out selectedAttitude))
            {
                Console.WriteLine("This is not a valid attitude. Please enter 'sad', 'happy', 'angry', or 'bored'.");
                Console.WriteLine("Enter attitude: ");
                Enum.TryParse(Console.ReadLine(), out selectedAttitude);
            }


            // add monsters with the sleected attitude to a new list
            foreach (Monster monster in monsters)
            {
                if (monster.Attitude == selectedAttitude)
                {
                    filteredMonsters.Add(monster);
                }
            }

            // display new list
            //
            // DISPLAY table headers            
            DisplayScreenHeader($"{selectedAttitude} Monsters");
            Console.WriteLine();
            Console.WriteLine(
               "Name".PadRight(15) +
               "Age".PadRight(15) +
               "Birthdate".PadRight(15) +
               "Attitude".PadRight(15) +
               "Tribe".PadRight(12) +
               "Active".PadRight(10) +
               "Greeting".PadRight(15)
               );
            Console.WriteLine(
               "-----".PadRight(15) +
               "-----".PadRight(15) +
               "-----".PadRight(15) +
               "-----".PadRight(15) +
               "------".PadRight(12) +
               "--------".PadRight(10) +
               "-------".PadRight(20)
               );

            foreach (Monster monster in filteredMonsters)
            {
                    Console.WriteLine(
                    monster.Name.ToString().PadRight(15) +
                    monster.Age.ToString().PadRight(15) +
                    monster.Birthdate.ToShortDateString().PadRight(15) +
                    monster.Attitude.ToString().PadRight(15) +
                    monster.MonsterTribe.ToString().PadRight(15) +
                    monster.Active.ToString().PadRight(8) +
                    monster.Greeting().ToString().PadRight(20)
                    );
            }

            DisplayContinuePrompt();
        }

        /// <summary>
        /// SCREEN: write list of monsters to data file
        /// </summary>
        /// <param name="monsters">list of monsters</param>
        static void DisplayWriteToDataFile(List<Monster> monsters)
        {
            DisplayScreenHeader("Write to Data File");

            //
            // prompt the user to continue
            //
            Console.WriteLine("\tThe application is ready to write to the data file.");
            Console.Write("\tEnter 'y' to continue or 'n' to cancel.");
            if (Console.ReadLine().ToLower() == "y")
            {
                DisplayContinuePrompt();
                WriteToDataFile(monsters);
                //
                // TODO process I/O exceptions
                //

                Console.WriteLine();
                Console.WriteLine("\tList written to the data file.");
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("\tList not written to the data file.");
            }

            DisplayContinuePrompt();
        }

        #endregion

        #region FILE I/O METHODS

        /// <summary>
        /// write monster list to data file
        /// </summary>
        /// <param name="monsters">list of monsters</param>
        static void WriteToDataFile(List<Monster> monsters)
        {
            string[] monstersString = new string[monsters.Count];

            //
            // create array of monster strings
            //
            for (int index = 0; index < monsters.Count; index++)
            {
                string monsterString =
                    monsters[index].Name + "," +
                    monsters[index].Age + "," +
                    monsters[index].Birthdate + "," +
                    monsters[index].Attitude + "," +
                    monsters[index].MonsterTribe + "," +
                    monsters[index].Active;
                monstersString[index] = monsterString;
            }

            File.WriteAllLines("Data\\Data.txt", monstersString);
        }

        /// <summary>
        /// read monsters from data file and return a list of monsters
        /// </summary>
        /// <returns>list of monsters</returns>        
        static List<Monster> ReadFromDataFile()
        {
            List<Monster> monsters = new List<Monster>();

            //
            // read all lines in the file
            //
            string[] monstersString = File.ReadAllLines("Data\\Data.txt");

            //
            // create monster objects and add to the list
            //
            foreach (string monsterString in monstersString)
            {
                //
                // get individual properties
                //
                string[] monsterProperties = monsterString.Split(',');

                //
                // create monster
                //
                Monster newMonster = new Monster();

                //
                // update monster property values
                //
                newMonster.Name = monsterProperties[0];

                int.TryParse(monsterProperties[1], out int age);
                newMonster.Age = age;

                DateTime.TryParse(monsterProperties[2], out DateTime birthdate);
                newMonster.Birthdate = birthdate;

                Enum.TryParse(monsterProperties[3], out Monster.EmotionalState attitude);
                newMonster.Attitude = attitude;

                Enum.TryParse(monsterProperties[4], out Monster.Tribe tribe);
                newMonster.MonsterTribe = tribe;

                bool.TryParse(monsterProperties[5], out bool active);
                newMonster.Active = active;

                //
                // add new monster to list
                //
                monsters.Add(newMonster);
            }

            return monsters;
        }

        #endregion

        #region CONSOLE HELPER METHODS

        /// <summary>
        /// display all monster properties
        /// </summary>
        /// <param name="monster">monster object</param>
        static void MonsterInfo(Monster monster)
        {
            Console.WriteLine($"\tName: {monster.Name}");
            Console.WriteLine($"\tAge: {monster.Age}");
            Console.WriteLine($"\tBirthdate: {monster.Birthdate:d}");
            Console.WriteLine($"\tAttitude: {monster.Attitude}");
            Console.WriteLine($"\tTribe: {monster.MonsterTribe}");
            Console.WriteLine($"\tActive? {monster.Active}");
            Console.WriteLine("\t" + monster.Greeting());
        }

        /// <summary>
        /// display welcome screen
        /// </summary>
        static void DisplayWelcomeScreen()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\tThe Monster Tracker");
            Console.WriteLine();

            DisplayContinuePrompt();
        }

        /// <summary>
        /// display closing screen
        /// </summary>
        static void DisplayClosingScreen()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\tThank you for using the Monster Tracker!");
            Console.WriteLine();

            DisplayContinuePrompt();
        }

        /// <summary>
        /// display continue prompt
        /// </summary>
        static void DisplayContinuePrompt()
        {
            Console.CursorVisible = false;
            Console.WriteLine();
            Console.Write("\tPress any key to continue.");
            Console.ReadKey();
            Console.CursorVisible = true;
        }

        /// <summary>
        /// display screen header
        /// </summary>
        static void DisplayScreenHeader(string headerText)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\t" + headerText);
            Console.WriteLine();
        }

        #endregion
    }
}
