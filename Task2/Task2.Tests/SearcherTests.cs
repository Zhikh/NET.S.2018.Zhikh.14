using System;
using NUnit.Framework;
using Task2.Logic;

namespace Task2.Tests
{
    [TestFixture]
    public class SearcherTests
    {
        #region Exceptions
        [Test]
        public void Search_NullComparer_ArgumentNullException()
          => Assert.Catch<ArgumentNullException>(() => BinarySearcher.Search(new int[] { 1, 2 }, 1, null));
        #endregion

        #region Search with three params
        [TestCase(4, 1, 2, 3, 4, 5, 6, ExpectedResult = 3)]
        [TestCase(0, 1, 2, 3, 4, 5, 6, ExpectedResult = -1)]
        [TestCase(1, 1, 1, 1, 1, 1, 1, ExpectedResult = 0)]
        [TestCase(3, 5, 3, 1, 4, 2, 7, ExpectedResult = 1)]
        [TestCase(6, 5, 3, 1, 4, 2, 7, ExpectedResult = -1)]
        [TestCase(6, 5, -3, 1, 4, 2, 7, 6, -6, ExpectedResult = 6)]
        [TestCase(int.MinValue, 5, int.MinValue, 1, int.MinValue, 2, 7, 6, -6, ExpectedResult = 1)]
        [TestCase(int.MaxValue, -5, int.MinValue, 1, int.MinValue, 2, 0, 7, 6, -6, int.MaxValue, ExpectedResult = 9)]
        [TestCase(-1, -5, int.MinValue, 1, int.MinValue, 2, 0, 7, 6, -6, int.MaxValue, ExpectedResult = -1)]
        public int Search_IntData_CorrectResult(int value, params int[] array)
        {
            return BinarySearcher.Search(array, value, new IntComparer());
        }
        #endregion
    }
}
