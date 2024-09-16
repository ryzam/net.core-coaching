using DemoConsoleApp1.PersonGroup;
using LibraryConsoleApp;
using System.Diagnostics;
using System.Threading;

namespace DemoConsoleApp1
{
    public class Program
    {
        static void Main(string[] args)
        {
            //Main Thread1
            Console.WriteLine($"Main Thread {Thread.CurrentThread?.ManagedThreadId}"); // Get from ThreadManagement
            AsyncAwaitDemo demo = new AsyncAwaitDemo();
           

            for (int i = 0; i < 100; i++)
            {
                demo.RunAsync(i);
            }
            Console.WriteLine("Demo"); // Thread 1


            Console.ReadLine();
        }


        
    }

    public class AsyncAwaitDemo
    {
        public async Task FetchDataAsync(int loop) //Thread 1
        {
            Console.WriteLine($"Loop {loop} - Thread FetchDataAsync using ThreadId {Thread.CurrentThread?.ManagedThreadId}");
            //Console.WriteLine("Starting to fetch data...");
            Console.WriteLine($"Loop {loop} - Relase {Thread.CurrentThread?.ManagedThreadId} because of await Task Delay");
            await Task.Delay(20000); //Thread akan release and free to handle other task  // Simulate a delay (e.g., waiting for an I/O operation)
           
            Console.WriteLine($"Loop {loop} - Continue using ThreadId {Thread.CurrentThread?.ManagedThreadId}");
         

            Console.WriteLine($"Loop {loop} Data fetched successfully.");
            //return 42;  // Return some fetched data
        }

        public async Task RunAsync(int loop)
        {
            Console.WriteLine($"Loop {loop } - Thread Before FetchDataAsync using ThreadId {Thread.CurrentThread?.ManagedThreadId}");
            //await Task.Delay(100); //Read database

            await FetchDataAsync(loop); // Thread 1 akan release and free to handle other task
            Console.WriteLine($"Loop {loop} - Thread After FetchDataAsync using ThreadId {Thread.CurrentThread?.ManagedThreadId}");
            Console.WriteLine($"Result");

        }
    }


}
