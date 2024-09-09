### **Inheritance and Polymorphism in C#**

Inheritance and polymorphism are core concepts of Object-Oriented Programming (OOP). They help developers write modular, flexible, and reusable code.

#### **1. Inheritance Basics (1 hour)**

- **Inheritance** is a mechanism where a class (derived class) can inherit properties and methods from another class (base class). It promotes code reuse and establishes a parent-child relationship between classes.

  - **Base Class**: The class whose members are inherited.
  - **Derived Class**: The class that inherits from the base class.

**Example of Inheritance:**

```csharp
public class Animal  // Base class
{
    public void Eat()
    {
        Console.WriteLine("Eating...");
    }
}

public class Dog : Animal  // Derived class
{
    public void Bark()
    {
        Console.WriteLine("Barking...");
    }
}

// Usage
Dog myDog = new Dog();
myDog.Eat();   // Inherited method
myDog.Bark();  // Method of derived class
```

In this example:
- `Dog` inherits from `Animal`. Therefore, `Dog` has access to the `Eat()` method defined in `Animal`.

#### **2. Method Overriding (1 hour)**

- **Method Overriding** allows a derived class to provide a specific implementation of a method that is already defined in its base class. To override a method, the base class method must be marked with the `virtual` keyword, and the derived class method must use the `override` keyword.

**Example of Method Overriding:**

```csharp
public class Animal
{
    public virtual void MakeSound()
    {
        Console.WriteLine("Animal makes a sound.");
    }
}

public class Dog : Animal
{
    public override void MakeSound()  // Overriding method
    {
        Console.WriteLine("Dog barks.");
    }
}

// Usage
Animal myAnimal = new Dog();
myAnimal.MakeSound();  // Output: "Dog barks."
```

In this example:
- The `Dog` class overrides the `MakeSound()` method of the `Animal` class to provide its specific implementation.

#### **3. Abstract Classes and Interfaces (1.5 hours)**

- **Abstract Class**: A class that cannot be instantiated and is designed to be a base class for other classes. It can have abstract methods (without implementation) and non-abstract methods (with implementation). Abstract methods must be implemented in derived classes.
  
- **Interface**: Defines a contract that classes can implement. An interface can only contain method signatures and properties but no implementation. A class can implement multiple interfaces, promoting multiple inheritance.

**Example of Abstract Classes:**

```csharp
public abstract class Shape
{
    public abstract void Draw();  // Abstract method
}

public class Circle : Shape
{
    public override void Draw()  // Implementing abstract method
    {
        Console.WriteLine("Drawing a circle.");
    }
}
```

**Example of Interfaces:**

```csharp
public interface IAnimal
{
    void Speak();  // Method signature
}

public class Dog : IAnimal
{
    public void Speak()  // Implementing interface method
    {
        Console.WriteLine("Dog barks.");
    }
}
```

#### **4. Polymorphism (1.5 hours)**

- **Polymorphism** means "many forms" and allows objects to be treated as instances of their parent class. The most common use of polymorphism in OOP is when a parent class reference is used to refer to a child class object.

- Polymorphism is achieved through:
  - **Method Overriding**: As shown above, allows a derived class to provide specific implementation.
  - **Interfaces and Abstract Classes**: Different classes can implement the same interface or inherit from an abstract class in different ways.

**Example of Polymorphism:**

```csharp
public class Animal
{
    public virtual void Speak()
    {
        Console.WriteLine("Animal speaks.");
    }
}

public class Dog : Animal
{
    public override void Speak()
    {
        Console.WriteLine("Dog barks.");
    }
}

public class Cat : Animal
{
    public override void Speak()
    {
        Console.WriteLine("Cat meows.");
    }
}

// Usage
Animal myAnimal1 = new Dog();
Animal myAnimal2 = new Cat();

myAnimal1.Speak();  // Output: "Dog barks."
myAnimal2.Speak();  // Output: "Cat meows."
```

#### **5. Hands-On: Extend the "Bank Account" Example with Different Account Types (2 hours)**

**Objective**: Extend the previous `BankAccount` class by creating different types of bank accounts like **SavingsAccount** and **CurrentAccount** using inheritance and polymorphism. Demonstrate method overriding and use an interface for common operations.

##### **Steps:**

1. **Create a Base Class `BankAccount`**:
   - The base class should contain common properties like `AccountNumber`, `AccountHolder`, `Balance`, and methods like `Deposit()`, `Withdraw()`, and `DisplayAccountInfo()`.

2. **Create Derived Classes `SavingsAccount` and `CurrentAccount`**:
   - **SavingsAccount** may have a minimum balance requirement.
   - **CurrentAccount** may have an overdraft facility.
   - Override the `Withdraw()` method to provide specific withdrawal logic for each account type.

3. **Use Polymorphism to Handle Different Account Types**:
   - Create a list of `BankAccount` objects and demonstrate polymorphism by calling overridden methods.

##### **Code Example:**

```csharp
using System;
using System.Collections.Generic;

namespace BankingApp
{
    // Base class
    public abstract class BankAccount
    {
        // Private fields
        private string accountNumber;
        private string accountHolder;
        protected double balance;  // Protected to allow derived classes to access it

        // Constructor
        public BankAccount(string accountNumber, string accountHolder, double initialBalance)
        {
            this.accountNumber = accountNumber;
            this.accountHolder = accountHolder;
            balance = initialBalance;
        }

        // Public properties
        public string AccountNumber => accountNumber;
        public string AccountHolder => accountHolder;
        public double Balance => balance;

        // Method to deposit money
        public virtual void Deposit(double amount)
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

        // Abstract method to withdraw money (must be implemented by derived classes)
        public abstract void Withdraw(double amount);

        // Method to display account information
        public virtual void DisplayAccountInfo()
        {
            Console.WriteLine($"Account Number: {accountNumber}");
            Console.WriteLine($"Account Holder: {accountHolder}");
            Console.WriteLine($"Balance: {balance:C}");
        }
    }

    // Derived class: SavingsAccount
    public class SavingsAccount : BankAccount
    {
        private double minimumBalance;

        // Constructor
        public SavingsAccount(string accountNumber, string accountHolder, double initialBalance, double minimumBalance)
            : base(accountNumber, accountHolder, initialBalance)
        {
            this.minimumBalance = minimumBalance;
        }

        // Overridden Withdraw method
        public override void Withdraw(double amount)
        {
            if (amount > 0 && (balance - amount) >= minimumBalance)
            {
                balance -= amount;
                Console.WriteLine($"Withdrew: {amount:C}. New Balance: {balance:C}");
            }
            else
            {
                Console.WriteLine("Invalid withdrawal amount or insufficient balance to maintain minimum balance.");
            }
        }

        // Overridden DisplayAccountInfo method
        public override void DisplayAccountInfo()
        {
            base.DisplayAccountInfo();
            Console.WriteLine($"Minimum Balance Requirement: {minimumBalance:C}");
        }
    }

    // Derived class: CurrentAccount
    public class CurrentAccount : BankAccount
    {
        private double overdraftLimit;

        // Constructor
        public CurrentAccount(string accountNumber, string accountHolder, double initialBalance, double overdraftLimit)
            : base(accountNumber, accountHolder, initialBalance)
        {
            this.overdraftLimit = overdraftLimit;
        }

        // Overridden Withdraw method
        public override void Withdraw(double amount)
        {
            if (amount > 0 && (balance + overdraftLimit) >= amount)
            {
                balance -= amount;
                Console.WriteLine($"Withdrew: {amount:C}. New Balance: {balance:C}");
            }
            else
            {
                Console.WriteLine("Invalid withdrawal amount or overdraft limit exceeded.");
            }
        }

        // Overridden DisplayAccountInfo method
        public override void DisplayAccountInfo()
        {
            base.DisplayAccountInfo();
            Console.WriteLine($"Overdraft Limit: {overdraftLimit:C}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Create different types of accounts
            BankAccount savings = new SavingsAccount("123456", "Alice", 5000.00, 1000.00);
            BankAccount current = new CurrentAccount("789012", "Bob", 3000.00, 2000.00);

            // Store accounts in a list
            List<BankAccount> accounts = new List<BankAccount> { savings, current };

            // Display account information and perform operations using polymorphism
            foreach (var account in accounts)
            {
                account.DisplayAccountInfo();
                account.Deposit(500);
                account.Withdraw(2000);
                account.DisplayAccountInfo();
                Console.WriteLine();
            }
        }
    }
}
```

**Key

 Concepts Demonstrated:**

- **Inheritance**: `SavingsAccount` and `CurrentAccount` inherit from the base class `BankAccount`.
- **Method Overriding**: Both derived classes override the `Withdraw()` method to provide specific functionality.
- **Abstract Classes**: `BankAccount` is an abstract class with an abstract method `Withdraw()`.
- **Polymorphism**: The `List<BankAccount>` stores different types of accounts, and method calls are resolved at runtime based on the actual object type.

This hands-on example demonstrates how to use inheritance and polymorphism to extend a base class into multiple specialized classes, providing a powerful way to handle different scenarios in OOP.
