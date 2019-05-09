using Console.Engines;
using Console.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Console
{
    class Program
    {
        private static BlockingCollection<string> printQueue = new BlockingCollection<string>();

        private static async Task<int> Main(string[] args)
        {
            var printThread = new Thread(() =>
            {
                while (true)
                {
                    System.Console.WriteLine(printQueue.Take());
                }
            })
            {
                IsBackground = true
            };
            printThread.Start();

            // Create a function to sum up all the even numbers in a supplied List parameter and return the result.
            var sum = sumEvenNumbers();

            // Create a function that will make an http GET request to a given URL and dump out the result in Console.
            GETRequest("http://localhost:5000/api/v1/time");

            // Create a function which will print out the numbers in a List to the console in a loop with a configurable delay.
            PrintNumbers(new List<int>() { 1, 2, 3, 4, 5 });

            return 0;
        }

        /// <summary>
        /// Create a function to sum up all the even numbers in a supplied List parameter and return the result.
        /// </summary>
        private static int sumEvenNumbers()
        {
            var calculator = new CalculatorExample(new Base10Calculator());
            var numbers = new List<int> { 1, 2, 3, 4, 5 };

            return calculator.AddEvenNumbers(numbers);
        }

        /// <summary>
        /// Create a function that will make an http GET request to a given URL and dump out the result in Console.
        /// </summary>
        private static void GETRequest(string url)
        {
            var client = new GETRequestExample(new HTTPRequestClient(), new ServerResponseLogContext());
            client.MakeRequestAsync(url).Wait();
            client.MakeRequestAsync(url, 200).Wait();
            client.MakeRequestAsync(url, 500).Wait();
        }


        /// <summary>
        /// Create a function which will print out the numbers in a List to the console in a loop with a configurable delay.
        /// </summary>
        private static void PrintNumbers(IEnumerable<int> numbers)
        {
            var tasks = new List<Task>
            {
                Task.Run(() => PrintRange(numbers, TimeSpan.FromMilliseconds(500))),
                Task.Run(() => PrintRange(numbers.Reverse(), TimeSpan.FromMilliseconds(1000)))
            };

            Task.WaitAll(tasks.ToArray());
        }

        private static Task PrintRange(IEnumerable<int> numbers, TimeSpan frequency)
        {
            foreach(var number in numbers)
            {
                printQueue.Add(number.ToString());
                Thread.Sleep(frequency);
            }

            return Task.CompletedTask;
        }

    }
}
