using Part1.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Part1
{
    /// <summary>
    /// A container to demonstrate calculator abilities through <seealso cref="ICalculator"/> injection.
    /// </summary>
    public class CalculatorExample
    {
        private readonly ICalculator calculator;

        public CalculatorExample(ICalculator calculator)
        {
            this.calculator = calculator;
        }

        public int AddEvenNumbers(IEnumerable<int> numbers)
        {
            return calculator.AddEvenNumbers(numbers);
        }
    }
}
