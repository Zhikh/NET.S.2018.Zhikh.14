using System;
using System.Collections.Generic;

namespace Task2.Logic
{
    public static class Searcher
    {
        #region Public API
        /// <summary>
        /// Gets array of T and element for searching 
        /// and find position of value in array
        /// </summary>
        /// <param name="array"> Unsorted/sorted array (it's not matter) </param>
        /// <param name="value"> Value for searching </param>
        /// <returns> Index of value in array </returns>
        /// <exception cref="ArgumentNullException"> When array or comparer are null </exception>
        public static int Search<T>(T value, params T[] array) where T : IComparable<T>
        {
            return 0;
        }

        /// <summary>
        /// Gets array of T and element for searching 
        /// and find position of value in array
        /// </summary>
        /// <param name="array"> Unsorted/sorted array (it's not matter) </param>
        /// <param name="value"> Value for searching </param>
        /// <param name="comparer"> Strategy of comparing values </param>
        /// <returns> Index of value in array </returns>
        /// <exception cref="ArgumentNullException"> When array or comparer are null </exception>
        public static int Search<T>(T value, IComparer<T> comparer, params T[] array)
        {
            return Search(value, comparer, new BinarySearcher<T>(), array);
        }

        /// <summary>
        /// Gets array of T and element for searching 
        /// and find position of value in array
        /// </summary>
        /// <param name="array"> Unsorted/sorted array (it's not matter) </param>
        /// <param name="value"> Value for searching </param>
        /// <param name="comparer"> Strategy of comparing values </param>
        /// <param name="search"> Strategy of searching elements </param>
        /// <returns> Index of value in array </returns>
        /// <exception cref="ArgumentNullException"> When array, search or comparer are null </exception>
        public static int Search<T>(T value, IComparer<T> comparer, ISearchStrategy<T> search, params T[] array)
        {
            if (search == null)
            {
                throw new ArgumentNullException($"The {nameof(search)} parameter can't be null!");
            }

            int[] indexes = new int[array.Length];
            FillArray(indexes);

            Sort(array, comparer, indexes);

            int index = search.SearchByBinary(array, value, comparer);

            if (index < 0)
            {
                return index;
            }

            return indexes[index];
        }

        private static void FillArray(int[] indexes)
        {
            for (int i = 0; i < indexes.Length; i++)
            {
                indexes[i] = i;
            }
        }
        #endregion

        #region Private methods
        private static void Sort<T>(T[] array, IComparer<T> comparer, int[] indexes)
        {
            bool isSwap;

            do
            {
                isSwap = false;

                for (int j = 0; j < array.Length - 1; j++)
                {
                    if (comparer.Compare(array[j + 1], array[j]) < 0)
                    {
                        Swap(ref array[j], ref array[j + 1]);
                        Swap(ref indexes[j], ref indexes[j + 1]);
                        isSwap = true;
                    }
                }
            }
            while (isSwap);
        }

        private static void Swap<T>(ref T first, ref T second)
        {
            T temp = first;

            first = second;
            second = temp;
        }
        #endregion
    }
}
