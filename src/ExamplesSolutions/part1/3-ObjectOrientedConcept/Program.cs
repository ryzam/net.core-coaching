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
            BankAccount account = new BankAccount("123456789", "Azam", 1000.00);

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