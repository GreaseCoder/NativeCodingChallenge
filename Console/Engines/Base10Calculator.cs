using Part1.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Part1.Engines
{
    /// <summary>
    /// A standard calculation engine for base 10 calculations.
    /// </summary>
    public class Base10Calculator : ICalculator
    {
        /// <summary>
        /// Given a collection of numbers, add only the even ones.
        /// </summary>
        /// <param name="numbers">A collection of numbers.</param>
        /// <returns>The sum of the even numbers in the collection.</returns>
        public int AddEvenNumbers(IEnumerable<int> numbers)
        {
            var summation = 0;
            foreach(var number in numbers)
            {
                if (number % 2 == 0)
                {
                    summation += number;
                }
            }

            return summation;
        }
    }
}
