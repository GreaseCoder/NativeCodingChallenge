using Part1.Engines;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Part1
{
    class Program
    {
        private static async Task<int> Main(string[] args)
        {
            sumEvenNumbers();

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

        }
    }
}
