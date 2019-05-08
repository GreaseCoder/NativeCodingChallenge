using System;
using System.Collections.Generic;
using System.Text;

namespace Console.Interfaces
{
    public interface ICalculator
    {
        /// <summary>
        /// Given a collection of numbers, add only the even ones.
        /// </summary>
        /// <param name="numbers">A collection of numbers.</param>
        /// <returns>The sum of the even numbers in the collection.</returns>
        int AddEvenNumbers(IEnumerable<int> numbers);
    }
}
