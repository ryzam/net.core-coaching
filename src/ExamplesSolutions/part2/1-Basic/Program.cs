using System;

namespace Basic
{
    class Program
    {
        static void Main(string[] args)
        {
            bool continueCalculation = true;

            while (continueCalculation)
            {
                Console.WriteLine("Simple Calculator");
                Console.WriteLine("Choose an operation:");
                Console.WriteLine("1. Addition (+)");
                Console.WriteLine("2. Subtraction (-)");
                Console.WriteLine("3. Multiplication (*)");
                Console.WriteLine("4. Division (/)");

                char operation = Console.ReadKey().KeyChar; // Read user input for the operation
                Console.WriteLine();

                // Get two numbers from the user
                Console.Write("Enter the first number: ");
                double num1 = Convert.ToDouble(Console.ReadLine());

                Console.Write("Enter the second number: ");
                double num2 = Convert.ToDouble(Console.ReadLine());

                double result = 0;

                // Perform the selected operation
                switch (operation)
                {
                    case '1':
                    case '+':
                        result = num1 + num2;
                        Console.WriteLine($"Result: {num1} + {num2} = {result}");
                        break;
                    case '2':
                    case '-':
                        result = num1 - num2;
                        Console.WriteLine($"Result: {num1} - {num2} = {result}");
                        break;
                    case '3':
                    case '*':
                        result = num1 * num2;
                        Console.WriteLine($"Result: {num1} * {num2} = {result}");
                        break;
                    case '4':
                    case '/':
                        if (num2 != 0)
                        {
                            result = num1 / num2;
                            Console.WriteLine($"Result: {num1} / {num2} = {result}");
                        }
                        else
                        {
                            Console.WriteLine("Error: Division by zero is not allowed.");
                        }
                        break;
                    default:
                        Console.WriteLine("Invalid operation selected. Please try again.");
                        break;
                }

                // Ask user if they want to perform another calculation
                Console.WriteLine("Do you want to perform another calculation? (y/n)");
                char continueInput = Console.ReadKey().KeyChar;
                Console.WriteLine();

                if (continueInput != 'y' && continueInput != 'Y')
                {
                    continueCalculation = false;
                }
            }

            Console.WriteLine("Thank you for using the Simple Calculator!");
        }
    }
}
