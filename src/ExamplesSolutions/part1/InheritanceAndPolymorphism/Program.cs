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
            BankAccount savings = new SavingsAccount("123456", "Umar", 5000.00, 1000.00);
            BankAccount current = new CurrentAccount("789012", "Azam", 3000.00, 2000.00);

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
