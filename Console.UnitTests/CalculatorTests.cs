using NUnit.Framework;
using Console.Engines;
using Console.Interfaces;
using System;
using System.Collections.Generic;

namespace Part1.UnitTests
{
    [TestFixture]
    public class CalculatorTests
    {
        private readonly ICalculator calculator = new Base10Calculator();

        [TestCase(1, 2, 3, 4, 5)]
        [TestCase(1, 5)]
        [TestCase(2)]
        public void AddEvenNumbers(params int[] numbers)
        {
            var factSummation = 0;
            foreach(var number in numbers)
            {
                if (number % 2 == 0)
                {
                    factSummation += number;
                }
            }

            var engineResult = calculator.AddEvenNumbers(numbers);

            Assert.AreEqual(factSummation, engineResult);
        }
    }
}
