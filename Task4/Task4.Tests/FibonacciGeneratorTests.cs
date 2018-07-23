using System;
using System.Collections.Generic;
using System.Numerics;
using NUnit.Framework;
using Task4.Logic;

namespace Task4.Tests
{
    [TestFixture]
    public class FibonacciGeneratorTests
    {
        [TestCase(0)]
        [TestCase(-1)]
        public void Generate_UncorrectData_ArgumentException(int count)
            => Assert.Catch<ArgumentException>(() => FibonacciGenerator.Generate(count));

        [TestCase(1, new int[] { 1 })]
        [TestCase(2, new int[] { 1, 1 })]
        [TestCase(5, new int[] { 1, 1, 2, 3, 5 })]
        [TestCase(8, new int[] { 1, 1, 2, 3, 5, 8, 13, 21 })]
        public void Generate_CorrectData_CorrectResult(int count, int[] expected)
        {
            int i = 0;
            foreach (var actual in FibonacciGenerator.Generate(count))
            {
                Assert.AreEqual((BigInteger)expected[i++], actual);
            }
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(5)]
        [TestCase(8)]
        [TestCase(50)]
        [TestCase(60)]
        [TestCase(70)]
        public void Generate_BigCount_CorrectResult(int count)
        {
            var sequence = FibonacciGenerator.Generate(count);

            BigInteger actual = 0;
            foreach (var element in sequence)
            {
                actual = element;
            }
            double _sqrtFromFive = Math.Sqrt(5);
            double firstExpression = (1 + _sqrtFromFive) / 2;
            double secondExpression = (1 - _sqrtFromFive) / 2;
            double innerExpression = Math.Pow(firstExpression, count) - Math.Pow(secondExpression, count);

            BigInteger expected = (BigInteger)(innerExpression/_sqrtFromFive);

            Assert.AreEqual(expected, actual);
        }
    }
}
