### **Object-Oriented Programming (OOP) Concepts in C#**

Object-Oriented Programming (OOP) is a paradigm that uses "objects" to design software. In C#, OOP helps create flexible, modular, and reusable code. The four main pillars of OOP are **Encapsulation**, **Inheritance**, **Polymorphism**, and **Abstraction**. Here, we'll focus on understanding **Classes and Objects**, **Properties and Methods**, **Constructors and Destructors**, and **Encapsulation**.

---

#### **1. Classes and Objects (1 hour)**

- **Class**: A blueprint for creating objects. A class defines properties (attributes) and methods (behaviors) that the objects created from the class will have.
- **Object**: An instance of a class. It represents an entity with specific values for the properties defined in the class.

**Example of a Class and Object:**

```csharp
public class Car // Class definition
{
    public string Brand;  // Property
    public string Model;  // Property

    public void DisplayInfo()  // Method
    {
        Console.WriteLine("Brand: " + Brand);
        Console.WriteLine("Model: " + Model);
    }
}

// Creating an object
Car myCar = new Car();
myCar.Brand = "Toyota";
myCar.Model = "Corolla";
myCar.DisplayInfo();
```

In the example above:
- `Car` is a class with properties `Brand` and `Model`, and a method `DisplayInfo()`.
- `myCar` is an object of the `Car` class.

#### **2. Properties and Methods (1 hour)**

- **Properties**: Represent the attributes of a class. They provide a mechanism to read, write, or compute private fields.
  - In C#, properties are often defined using `get` and `set` accessors.
- **Methods**: Define the behavior or functionality of a class. They are functions defined inside a class.

**Example of Properties and Methods:**

```csharp
public class Person
{
    // Auto-implemented properties
    public string Name { get; set; }
    public int Age { get; set; }

    // Method
    public void Greet()
    {
        Console.WriteLine($"Hello, my name is {Name} and I am {Age} years old.");
    }
}

Person person = new Person();
person.Name = "Alice";
person.Age = 25;
person.Greet();
```

#### **3. Constructors and Destructors (1 hour)**

- **Constructor**: A special method that is called when an object is instantiated. It is used to initialize objects. A constructor has the same name as the class and no return type.
  - **Default Constructor**: Provided by C# if no constructors are defined.
  - **Parameterized Constructor**: Allows you to pass parameters when creating an object.

- **Destructor**: A special method that is called when an object is destroyed or finalized. It is defined using a tilde `~` followed by the class name. In C#, destructors are rarely used explicitly as garbage collection is automatic.

**Example of Constructors and Destructors:**

```csharp
public class Animal
{
    public string Name { get; set; }

    // Constructor
    public Animal(string name)
    {
        Name = name;
        Console.WriteLine($"{Name} is created.");
    }

    // Destructor
    ~Animal()
    {
        Console.WriteLine($"{Name} is destroyed.");
    }
}

Animal animal = new Animal("Lion");
```

#### **4. Encapsulation (1 hour)**

- **Encapsulation**: The concept of bundling the data (properties) and methods that operate on the data into a single unit or class. It restricts direct access to some of an object's components and helps protect the integrity of the object's state.
  - **Access Modifiers**:
    - `public`: Accessible from anywhere.
    - `private`: Accessible only within the class.
    - `protected`: Accessible within the class and derived classes.
    - `internal`: Accessible within the same assembly.

**Example of Encapsulation:**

```csharp
public class Employee
{
    // Private field
    private double salary;

    // Public property
    public double Salary
    {
        get { return salary; }
        set
        {
            if (value > 0)
                salary = value;
        }
    }

    // Method to display salary
    public void DisplaySalary()
    {
        Console.WriteLine("Employee Salary: " + Salary);
    }
}
```

In this example:
- The `salary` field is private, meaning it cannot be accessed directly from outside the class.
- The `Salary` property is public, and it provides controlled access to the `salary` field through `get` and `set` accessors.

---

### **Hands-On: Design and Implement a "Bank Account" Class (2 hours)**

**Objective**: Create a class named `BankAccount` that represents a bank account with properties for account number, account holder, and balance. Include methods to deposit, withdraw, and display account information.

**Steps:**

1. **Define the `BankAccount` Class**:
   - Create a class named `BankAccount` with private fields for `accountNumber`, `accountHolder`, and `balance`.
   - Implement public properties to provide controlled access to these fields.

2. **Add a Constructor**:
   - Add a constructor to initialize the account number, account holder, and balance.

3. **Implement Methods**:
   - Create methods for `Deposit()`, `Withdraw()`, and `DisplayAccountInfo()`.

4. **Demonstrate Encapsulation**:
   - Use access modifiers to encapsulate the data properly.

**Example Code for `BankAccount` Class:**

```csharp
using System;

namespace BankingApp
{
    public class BankAccount
    {
        // Private fields
        private string accountNumber;
        private string accountHolder;
        private double balance;

        // Constructor to initialize account details
        public BankAccount(string accountNumber, string accountHolder, double initialBalance)
        {
            this.accountNumber = accountNumber;
            this.accountHolder = accountHolder;
            balance = initialBalance;
        }

        // Public property for account number (read-only)
        public string AccountNumber
        {
            get { return accountNumber; }
        }

        // Public property for account holder (read-only)
        public string AccountHolder
        {
            get { return accountHolder; }
        }

        // Public property for balance (read-only)
        public double Balance
        {
            get { return balance; }
        }

        // Method to deposit money
        public void Deposit(double amount)
        {
            if (amount > 0)
            {
                balance += amount;
                Console.WriteLine($"Deposited: {amount:C}. New Balance: {balance:C}");
            }
            else
            {
                Console.WriteLine("Deposit amount must be positive.");
            }
        }

        // Method to withdraw money
        public void Withdraw(double amount)
        {
            if (amount > 0 && amount <= balance)
            {
                balance -= amount;
                Console.WriteLine($"Withdrew: {amount:C}. New Balance: {balance:C}");
            }
            else
            {
                Console.WriteLine("Invalid withdrawal amount or insufficient balance.");
            }
        }

        // Method to display account information
        public void DisplayAccountInfo()
        {
            Console.WriteLine($"Account Number: {accountNumber}");
            Console.WriteLine($"Account Holder: {accountHolder}");
            Console.WriteLine($"Balance: {balance:C}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Create a new bank account object
            BankAccount account = new BankAccount("123456789", "John Doe", 1000.00);

            // Display account information
            account.DisplayAccountInfo();

            // Perform deposit and withdrawal operations
            account.Deposit(500.00);
            account.Withdraw(200.00);
            account.Withdraw(1500.00); // Invalid operation

            // Display final account information
            account.DisplayAccountInfo();
        }
    }
}
```

**Key Concepts Demonstrated:**
- **Class and Object**: `BankAccount` is a class, and `account` is an object of that class.
- **Properties and Methods**: The class has properties (`AccountNumber`, `AccountHolder`, `Balance`) and methods (`Deposit()`, `Withdraw()`, `DisplayAccountInfo()`).
- **Constructor**: Initializes the `BankAccount` object with necessary details.
- **Encapsulation**: Private fields and public properties/methods provide controlled access to the class data.
- **Error Handling**: Simple validation is added to ensure proper deposit and withdrawal operations.

---

This hands-on example covers the core concepts of OOP in C# and provides a practical implementation of a real-world problem using classes, objects, properties, methods, constructors, destructors, and encapsulation.
