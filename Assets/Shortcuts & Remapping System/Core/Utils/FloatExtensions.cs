using System;
using UnityEngine;

namespace RedRats.Core
{
    /// <summary>
    /// Extension methods for the float type.
    /// </summary>
    public static class FloatExtensions
    {
        /// <summary>
        /// Works like Mathf.Sign but with a 0.
        /// </summary>
        public static int Sign0(this float number)
        {
            return number == 0 ? 0 : number > 0 ? 1 : -1;
        }

        /// <summary>
        /// Converts seconds to milliseconds.
        /// </summary>
        /// <param name="seconds">The seconds to delay.</param>
        /// <returns>Seconds in milliseconds.</returns>
        public static int ToMilliseconds(this float seconds)
        {
            return (int) seconds * 1000;
        }
        
        /// <summary>
        /// Round a float value to a specific amount of decimal places.
        /// </summary>
        /// <param name="value">The value to round.</param>
        /// <param name="decimals">Amount of decimals.</param>
        /// <returns></returns>
        public static float Round(this float value, int decimals = 2)
        {
            float multiplier = Mathf.Pow(10.0f, decimals);
            return Mathf.Round(value * multiplier) / multiplier;
        }
        
        /// <summary>
        /// Round a float value to a specific amount of decimal places.
        /// </summary>
        /// <param name="value">The value to round.</param>
        /// <param name="decimalMultiplier">The decimal multiplier to use.</param>
        /// <returns></returns>
        public static float RoundM(this float value, int decimalMultiplier)
        {
            return Mathf.Round(value * decimalMultiplier) / decimalMultiplier;
        }
        
        /// <summary>
        /// Returns TRUE if 2 <see cref="float"/>s are the same.
        /// <p>Uses <see cref="float"/></p>.Distance to measure sameness.
        /// </summary>
        /// <param name="number">Float A</param>
        /// <param name="other">Float B</param>
        /// <param name="tolerance">How far from each other can the floats be, to be considered the same.</param>
        /// <returns>TRUE if floats are the same.</returns>
        public static bool IsSameAs(this float number, float other, float tolerance = 0.001f)
        {
            return Math.Abs(number - other) < tolerance;
        }
        
        /// <summary>
        /// Remaps a value from one range to another.
        /// </summary>
        /// <param name="value">The value to remap</param>
        /// <param name="from1">First Range min value.</param>
        /// <param name="to1">First range max value.</param>
        /// <param name="from2">Target range min value.</param>
        /// <param name="to2">target range max value.</param>
        /// <returns>The remapped value.</returns>
        public static float Remap(this float value, float from1, float from2, float to1, float to2)
        {
            return to1 + (value - from1) * (to2 - to1) / (from2 - from1);
        }
    }
}