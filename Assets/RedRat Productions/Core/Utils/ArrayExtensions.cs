using System;

namespace RedRats.Core
{
    /// <summary>
    /// Contains extension methods for arrays.
    /// </summary>
    public static class ArrayExtensions
    {
        /// <summary>
        /// Returns a duplicate instance of an array.
        /// </summary>
        /// <param name="array">The array to duplicate.</param>
        /// <typeparam name="T">Any type.</typeparam>
        /// <returns>A new array with same elements as the original.</returns>
        public static T[] AsCopy<T>(this T[] array)
        {
            T[] copyArray = new T[array.Length];
            Array.Copy(array, copyArray, array.Length);
            return copyArray;
        }
    }
}