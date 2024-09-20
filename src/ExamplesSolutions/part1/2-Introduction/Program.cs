using System; // Importing the System namespace

namespace Introuction // Defining a namespace
{
    class Program // Defining a class named Program
    {
        // Main method: Entry point of the application
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!"); // Output text to the console

            //Variable and datatypes
            int age = 25; // Integer type
            string name = "Azam"; // String type
            bool isStudent = true; // Boolean type

            Console.WriteLine($"Name: {name} Age: {age} IsStudent: {isStudent}");
            Console.WriteLine("");

            //Operators
            int x = 10;
            int y = 20;
            int sum = x + y; // Addition operator

            Console.WriteLine($"Addition x + y = {sum}");
            Console.WriteLine("");

            //Control Structure
            Console.Write("Enter your age: "); // Prompt user for input
            int currentAge = Int32.Parse(Console.ReadLine()); // Read user input

            if (age > 18)
            {
                Console.WriteLine("Adult");
            }
            else
            {
                Console.WriteLine("Minor");
            }
        }
    }
}
