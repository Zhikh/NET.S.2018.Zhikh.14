using System;
using NUnit.Framework;
using Task4.Logic;

namespace Task4.Tests
{
    [TestFixture]
    public class FibonacciGeneratorTests
    {
        [TestCase(2, 0)]
        [TestCase(3, -1)]
        public void Generate_UncorrectData_ArgumentException(int startIndex, int count)
            => Assert.Catch<ArgumentException>(() => FibonacciGenerator.Generate(startIndex, count));

        [TestCase(0, 1, ExpectedResult = new int[] { 0 })]
        [TestCase(-1, 1, ExpectedResult = new int[] { 1 })]
        [TestCase(-2, 1, ExpectedResult = new int[] { -1 })]
        [TestCase(0, 5, ExpectedResult = new int[] { 0, 1, 1, 2, 3 })]
        [TestCase(0, 5, ExpectedResult = new int[] { 0, 1, 1, 2, 3 })]
        [TestCase(-3, 8, ExpectedResult = new int[] { 2, -1, 1, 0, 1, 1, 2, 3 })]
        [TestCase(-10, 6, ExpectedResult = new int[] { -55, 34, -21, 13, -8, 5 })]
        public int[] Generate_CorrectData_CorrectResult(int startIndex, int count) 
            => FibonacciGenerator.Generate(startIndex, count);
    }
}
