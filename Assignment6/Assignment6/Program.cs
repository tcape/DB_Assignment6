using System;
using System.Data.SqlClient;


namespace Assignment6
{
    class Program
    {        
        static void Main(string[] args)
        {
            var d = new Driver();
            string choice = null;
            do
            {
                choice = null;
                Console.Clear();
                d.MainMenu();
                if (!d.quit)
                {
                    Console.WriteLine("\n\nStart another query? (y/n)");
                    choice = Console.ReadLine();
                    choice = choice.ToUpper();
                    while (choice != "Y" && choice != "N")
                    {
                        Console.WriteLine("Invalid choice. Enter: y or n");
                        choice = Console.ReadLine();
                        choice = choice.ToUpper();
                    }
                }
            }
            while (choice == "Y");
        }
    }
}

           