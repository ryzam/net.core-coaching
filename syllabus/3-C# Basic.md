Here's a detailed breakdown of **C# Basics**, focusing on **variables, data types, and operators**, **control structures**, **arrays and collections**, and a **hands-on project** to create a simple calculator program.

---

### **C# Basics**

#### **1. Variables, Data Types, and Operators (1.5 hours)**

**Variables and Data Types:**
- **Variables** are used to store data in a program. Each variable must have a specific data type that determines the kind of data it can hold.
- **Data Types** in C# are broadly categorized into:
  - **Value Types**: These directly contain data. Examples include:
    - `int`: Stores integers (e.g., `int age = 25;`).
    - `double`: Stores floating-point numbers (e.g., `double price = 99.99;`).
    - `char`: Stores single characters (e.g., `char grade = 'A';`).
    - `bool`: Stores boolean values (`true` or `false`).
  - **Reference Types**: These store references to the actual data. Examples include:
    - `string`: Stores a sequence of characters (e.g., `string name = "John";`).
    - `object`: The base type from which all other types derive.

- **Variable Declaration and Initialization:**
  ```csharp
  int age = 30; // Declaration and initialization
  string name = "Alice"; // Declaration and initialization
  ```

**Operators in C#:**
- **Arithmetic Operators**: Used for mathematical operations.
  - `+` (addition), `-` (subtraction), `*` (multiplication), `/` (division), `%` (modulus)
  ```csharp
  int sum = 5 + 3; // 8
  int product = 5 * 3; // 15
  ```
- **Comparison Operators**: Used to compare two values.
  - `==` (equal to), `!=` (not equal to), `<` (less than), `>` (greater than), `<=` (less than or equal to), `>=` (greater than or equal to)
  ```csharp
  bool isEqual = (5 == 3); // false
  ```
- **Logical Operators**: Used to combine conditional statements.
  - `&&` (logical AND), `||` (logical OR), `!` (logical NOT)
  ```csharp
  bool result = (5 > 3) && (8 > 5); // true
  ```

#### **2. Control Structures (If, Switch, Loops) (2 hours)**

Control structures control the flow of execution in a program.

**Conditional Statements:**
- **`if` Statement**: Executes a block of code if the condition is true.
  ```csharp
  int number = 5;
  if (number > 0)
  {
      Console.WriteLine("Positive number");
  }
  ```
- **`else if` and `else` Statements**: Provides alternative conditions.
  ```csharp
  if (number > 0)
  {
      Console.WriteLine("Positive number");
  }
  else if (number < 0)
  {
      Console.WriteLine("Negative number");
  }
  else
  {
      Console.WriteLine("Zero");
  }
  ```

**Switch Statement:**
- The **`switch` statement** selects a block of code to execute based on the value of a variable.
  ```csharp
  char grade = 'B';
  switch (grade)
  {
      case 'A':
          Console.WriteLine("Excellent");
          break;
      case 'B':
          Console.WriteLine("Good");
          break;
      case 'C':
          Console.WriteLine("Average");
          break;
      default:
          Console.WriteLine("Invalid grade");
          break;
  }
  ```

**Loops:**
- **`for` Loop**: Repeats a block of code a specific number of times.
  ```csharp
  for (int i = 0; i < 5; i++)
  {
      Console.WriteLine("Iteration: " + i);
  }
  ```
- **`while` Loop**: Repeats a block of code as long as a condition is true.
  ```csharp
  int count = 0;
  while (count < 5)
  {
      Console.WriteLine("Count: " + count);
      count++;
  }
  ```
- **`do-while` Loop**: Similar to `while`, but ensures the code block is executed at least once.
  ```csharp
  int number = 0;
  do
  {
      Console.WriteLine("Number: " + number);
      number++;
  } while (number < 5);
  ```

#### **3. Arrays and Collections (1.5 hours)**

**Arrays:**
- Arrays store a fixed-size sequence of elements of the same type.
  - **Declaration and Initialization**:
    ```csharp
    int[] numbers = { 1, 2, 3, 4, 5 };
    Console.WriteLine(numbers[0]); // Output: 1
    ```
  - **Accessing Elements**:
    ```csharp
    numbers[2] = 10; // Changes the third element to 10
    ```
  - **Iterating Through Arrays**:
    ```csharp
    foreach (int num in numbers)
    {
        Console.WriteLine(num);
    }
    ```

**Collections:**
- Collections are more flexible than arrays; they can grow in size and provide more functionality.
  - **List**: A dynamic array that can grow or shrink.
    ```csharp
    List<string> fruits = new List<string> { "Apple", "Banana", "Orange" };
    fruits.Add("Mango"); // Add an element
    Console.WriteLine(fruits.Count); // Get the count of elements
    ```
  - **Dictionary**: A collection of key-value pairs.
    ```csharp
    Dictionary<int, string> studentGrades = new Dictionary<int, string>();
    studentGrades.Add(1, "A");
    studentGrades.Add(2, "B");
    Console.WriteLine(studentGrades[1]); // Output: A
    ```
  - Other collections include **Queue**, **Stack**, and **HashSet**.

#### **4. Hands-On: Create a Simple Calculator Program (2 hours)**

**Objective**: Build a console-based calculator that performs basic arithmetic operations like addition, subtraction, multiplication, and division.

**Steps:**

1. **Set Up the Console Application**:
   - Create a new **Console App** project in Visual Studio or Visual Studio Code.

2. **Design the Calculator Logic**:
   - Use `if`, `else if`, or `switch` statements to determine the operation to perform.
   - Use `while` or `do-while` loops to allow continuous operations until the user decides to exit.

3. **Implement User Input and Output**:
   - Use `Console.ReadLine()` to get user input and `Console.WriteLine()` to display results.

4. **Code Example**:

```csharp
using System;

namespace SimpleCalculator
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
```

**Key Concepts in the Example:**
- **Reading user input** and converting it to appropriate data types

.
- **Using switch-case statements** to select operations.
- **Performing arithmetic operations** and handling **division by zero** errors.
- **Looping** for continuous calculation until the user decides to exit.

---

This detailed breakdown provides a solid foundation for understanding the basics of C# programming, including variables, data types, operators, control structures, and collections. The hands-on calculator project ties together these concepts in a practical application.
