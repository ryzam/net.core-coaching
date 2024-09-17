using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ParallelWithAsync
{

    public class ParallelAsyncExample
    {
        private static readonly HttpClient httpClient = new HttpClient();

        public async Task<string> FetchDataAsync(string url)
        {
            Console.WriteLine($"Starting fetch from {url} on thread {Thread.CurrentThread.ManagedThreadId}");
            HttpResponseMessage response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            Console.WriteLine($"Completed fetch from {url} on thread {Thread.CurrentThread.ManagedThreadId}");
            return await response.Content.ReadAsStringAsync();
        }

        public void RunParallelAsyncCalls()
        {
            List<string> urls = new List<string>
        {
            "https://jsonplaceholder.typicode.com/posts/1",
            "https://jsonplaceholder.typicode.com/posts/2",
            "https://jsonplaceholder.typicode.com/posts/3"
        };

            Parallel.ForEach(urls, async (url) =>
            {
                await FetchDataAsync(url);
            });
        }

        public static void Main(string[] args)
        {
            ParallelAsyncExample example = new ParallelAsyncExample();
            example.RunParallelAsyncCalls();

            // Prevent the console app from closing immediately
            Console.ReadLine();
        }
    }
}
