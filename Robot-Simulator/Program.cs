using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robot_Simulator
{
    class Program
    {
        #region fields
        private const string ExitCommand = "exit";

        private const string Prompt = ">> ";
        #endregion
        #region main
        static void Main(string[] args)
        {
            displaywelcomemsg();
            var driver = new RobotDriver(new Robot());
            while (true)
            {
                string command = askforcommand();
                if(!string.IsNullOrEmpty(command))
                {
                   
                    if (command.ToUpper() == "EXIT" || command.ToUpper() == "QUIT")
                    {
                        Environment.Exit(0);
                    }
                    Console.WriteLine(driver.executecommand(command));

                }
                
            }

        }
        #endregion
        #region methods
        //display welcome message and all list of commands
        static void displaywelcomemsg()
        {
            Console.WriteLine("welcome");
            Console.WriteLine("Robot created on {0}x{1} grid", 5, 5);
            Console.WriteLine("Available commands:");
            Console.WriteLine(ExitCommand);
            foreach (string cmd in Enum.GetNames(typeof(Instruction)))
            {
                Console.WriteLine(cmd);
            }
        }
        //ask for user input to enter different commands 
        static string askforcommand()
        {
            Console.WriteLine(Prompt);
            return Console.ReadLine();
        }
        
        #endregion
    }
}
