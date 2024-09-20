using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading.Tasks;

namespace ConcurrentAndParallel
{
    public class CombinedConcurrentAndParallel
    {
        private static readonly HttpClient httpClient = new HttpClient();

        public async Task RunCombinedAsync()
        {
            List<string> urls = new List<string>
            {
                "https://jsonplaceholder.typicode.com/posts/1",
                "https://jsonplaceholder.typicode.com/posts/2",
                "https://jsonplaceholder.typicode.com/posts/3"
            };

            // Step 1: Concurrently fetch data from multiple URLs
            Task<string>[] fetchTasks = new Task<string>[urls.Count];
            for (int i = 0; i < urls.Count; i++)
            {
                fetchTasks[i] = FetchDataAsync(urls[i]);
            }

            string[] results = await Task.WhenAll(fetchTasks);

            // Step 2: Process each result in parallel
            Parallel.ForEach(results, result =>
            {
                ProcessData(result);
            });
        }

        private async Task<string> FetchDataAsync(string url)
        {
            HttpResponseMessage response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        private void ProcessData(string data)
        {
            // Simulate a CPU-bound operation on the fetched data
            Console.WriteLine($"Processing data of length: {data.Length}");
        }

        public static async Task Main(string[] args)
        {
            CombinedConcurrentAndParallel example = new CombinedConcurrentAndParallel();
            await example.RunCombinedAsync();
        }
    }
}
