using System;
using System.Collections.Generic;

namespace Task2.Logic
{
    public static class Searcher<T> where T : IComparer<T>
    {
        #region Public API
        /// <summary>
        /// Gets array of T and element for searching 
        /// and find position of value in array
        /// </summary>
        /// <param name="array"> Unsorted/sorted array (it's not matter) </param>
        /// <param name="value"> Value for searching </param>
        /// <returns> Index of value in array </returns>
        public static int Search(T[] array, T value, IComparer<T> comparer, ISearchStrategy<T> search)
        {
            Array.Sort(array, comparer);

            return search.SearchByBinary(array, value, comparer);
        }
        #endregion
    }
}
