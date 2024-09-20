using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace AsyncWithoutAwaitExample
{
    public class AsyncWithoutAwait
    {
        private static readonly HttpClient httpClient = new HttpClient();

        // An async method that fetches data from a URL
        public async Task<string> FetchDataAsync(string url)
        {
            Console.WriteLine($"Starting fetch from {url} on thread {Thread.CurrentThread.ManagedThreadId}");

            // Simulate asynchronous I/O-bound operation
            HttpResponseMessage response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            Console.WriteLine($"Completed fetch from {url} on thread {Thread.CurrentThread.ManagedThreadId}");
            return await response.Content.ReadAsStringAsync();
        }

        public void RunParallelFetchWithoutAwait()
        {
            List<string> urls = new List<string>
        {
            "https://jsonplaceholder.typicode.com/posts/1",
            "https://jsonplaceholder.typicode.com/posts/2",
            "https://jsonplaceholder.typicode.com/posts/3"
        };

            List<Task<string>> tasks = new List<Task<string>>();

            // Calling async method in parallel without awaiting
            foreach (var url in urls)
            {
                Task<string> fetchTask = FetchDataAsync(url);  // Call without await
                tasks.Add(fetchTask);  // Collect tasks
            }

            // Tasks are running concurrently; awaiting them to get results
            Task.WhenAll(tasks).Wait();  // Block here to wait for all tasks to complete

            Console.WriteLine("All tasks completed.");
        }

        public static void Main(string[] args)
        {
            AsyncWithoutAwait example = new AsyncWithoutAwait();
            example.RunParallelFetchWithoutAwait();
        }
    }
}
