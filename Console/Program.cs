using Console.Engines;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Console
{
    class Program
    {
        private static async Task<int> Main(string[] args)
        {
            var sum = sumEvenNumbers();
            GETRequest(@"https://baconipsum.com/api/?type=meat-and-filler");

            return 0;
        }

        private static int sumEvenNumbers()
        {
            var calculator = new CalculatorExample(new Base10Calculator());
            var numbers = new List<int> { 1, 2, 3, 4, 5 };
            return calculator.AddEvenNumbers(numbers);
        }

        private static void GETRequest(string url)
        {
            var client = new GETRequestExample(new HTTPRequestClient());
            client.MakeRequest(url).Wait();
        }
    }
}
