using RedRats.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RedRats.Core
{
    /// <summary>
    /// Contains various methods for checking correctness of parameters. If a method fails to pass, it throws an exception.
    /// </summary>
    public static class Preconditions
    {
        public static event Action<string> OnFireErrorMessage;

        #region Object Checks

        /// <summary>
        /// Ensures a given object is not null.
        /// </summary>
        /// <param name="value">The object to check.</param>
        /// <param name="variableName">Name of the checked variable.</param>
        /// <param name="customMessage">The message of the error. If blank will use default.</param>
        /// <exception cref="PreconditionException"></exception>
        public static void IsNotNull(object value, string variableName, string customMessage = "")
        {
            if (value == null)
            {
                ThrowException(s => new PreconditionException(s), customMessage, $"'{variableName}' cannot be null.");
            }
        }

        /// <summary>
        /// Ensures a given object is of a specific type.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="variableName"></param>
        /// <param name="customMessage"></param>
        /// <typeparam name="T"></typeparam>
        public static void IsType<T>(object value, string variableName, string customMessage = "")
        {
            if (value is not T)
            {
                ThrowException(s => new PreconditionException(s), customMessage, $"{variableName} must be of type {typeof(T)}. ({value.GetType()})");
            }
        }
        
        #endregion

        #region Int Checks

        /// <summary>
        /// Ensures an number is not equal to a specific value.
        /// </summary>
        /// <param name="value">The number to check.</param>
        /// <param name="allowedValue">The value it cannot equal to.</param>
        /// <param name="variableName">Name of the checked variable.</param>
        /// /// <param name="customMessage">The message of the error. If blank will use default.</param>
        public static void IsIntEqual(int value, int allowedValue, string variableName, string customMessage = "")
        {
            if (value != allowedValue) ThrowException(s => new PreconditionException(s), customMessage, $"{variableName} '{value.ToString()}' must equal {allowedValue.ToString()}.");
        }
        
        /// <summary>
        /// Ensures an number is not equal to a specific value.
        /// </summary>
        /// <param name="value">The number to check.</param>
        /// <param name="disallowedValue">The value it cannot equal to.</param>
        /// <param name="variableName">Name of the checked variable.</param>
        /// /// <param name="customMessage">The message of the error. If blank will use default.</param>
        public static void IsIntNotEqual(int value, int disallowedValue, string variableName, string customMessage = "")
        {
            if (value == disallowedValue)
            {
                ThrowException(s => new PreconditionException(s), customMessage, $"{variableName} '{value.ToString()}' cannot equal {disallowedValue.ToString()}.");
            }
        }

        /// <summary>
        /// Ensures an number is bigger than a specific value.
        /// </summary>
        /// <param name="value">The number to check.</param>
        /// <param name="minSize">Minimum value allowed.</param>
        /// <param name="variableName">Name of the checked variable.</param>
        /// <param name="customMessage">The message of the error. If blank will use default.</param>
        public static void IsIntBiggerThan(int value, int minSize, string variableName, string customMessage = "")
        {
            if (value <= minSize)
            {
                ThrowException(s => new PreconditionException(s), customMessage, $"{variableName} '{value.ToString()}' must be above {minSize.ToString()}.");
            }
        }

        /// <summary>
        /// Ensures an number is bigger than or equal to a specific value.
        /// </summary>
        /// <param name="value">The number to check.</param>
        /// <param name="minSize">Minimum value allowed.</param>
        /// <param name="variableName">Name of the checked variable.</param>
        /// <param name="customMessage">The message of the error. If blank will use default.</param>
        public static void IsIntBiggerOrEqualTo(int value, int minSize, string variableName, string customMessage = "")
        {
            if (value < minSize)
            {
                ThrowException(s => new PreconditionException(s), customMessage, $"{variableName} '{value.ToString()}' must be above or equal {minSize.ToString()}.");
            }
        }

        /// <summary>
        /// Ensures an number is lower than a specific value.
        /// </summary>
        /// <param name="value">The number to check.</param>
        /// <param name="maxSize">Maximum value allowed.</param>
        /// <param name="variableName">Name of the checked variable.</param>
        /// <param name="customMessage">The message of the error. If blank will use default.</param>
        public static void IsIntLowerThan(int value, int maxSize, string variableName, string customMessage = "")
        {
            if (value >= maxSize)
            {
                ThrowException(s => new PreconditionException(s), customMessage, $"{variableName} {value.ToString()} must be below {maxSize.ToString()}.");
            }
        }

        /// <summary>
        /// Ensures an number is lower than or equal to a specific value.
        /// </summary>
        /// <param name="value">The number to check.</param>
        /// <param name="maxSize">Maximum value allowed.</param>
        /// <param name="variableName">Name of the checked variable.</param>
        /// <param name="customMessage">The message of the error. If blank will use default.</param>
        public static void IsIntLowerOrEqualTo(int value, int maxSize, string variableName, string customMessage = "")
        {
            if (value > maxSize)
            {
                ThrowException(s => new PreconditionException(s), customMessage, $"{variableName} '{value.ToString()}' must be below or equal {maxSize.ToString()}.");
            }
        }

        /// <summary>
        /// Ensures an number is within a given range (both inclusive).
        /// </summary>
        /// <param name="value">The number to check.</param>
        /// <param name="lowBounds">Minimum value allowed.</param>
        /// <param name="highBounds">Maximum value allowed.</param>
        /// <param name="variableName">Name of the checked variable.</param>
        /// <param name="customMessage">The message of the error. If blank will use default.</param>
        public static void IsIntInRange(int value, int lowBounds, int highBounds, string variableName, string customMessage = "")
        {
            if (value < lowBounds && value > highBounds)
            {
                ThrowException(s => new PreconditionException(s), customMessage, $"{variableName} '{value.ToString()}' must be in between {lowBounds.ToString()} - {highBounds.ToString()}.");
            }
        }

        #endregion

        #region Float Checks

        /// <summary>
        /// Ensures a number is not equal to a specific value.
        /// </summary>
        /// <param name="value">The number to check.</param>
        /// <param name="allowedValue">The value it cannot equal to.</param>
        /// <param name="variableName">Name of the checked variable.</param>
        /// <param name="customMessage">The message of the error. If blank will use default.</param>
        public static void IsFloatEqual(float value, float allowedValue, string variableName, string customMessage = "")
        {
            if (Math.Abs(value - allowedValue) > 0.01f) 
            {
                ThrowException(s => new PreconditionException(s), customMessage, $"{variableName} '{value.ToString()}' must equal {allowedValue.ToString()}.");
            }
        }
        
        /// <summary>
        /// Ensures a number is not equal to a specific value.
        /// </summary>
        /// <param name="value">The number to check.</param>
        /// <param name="disallowedValue">The value it cannot equal to.</param>
        /// <param name="variableName">Name of the checked variable.</param>
        /// <param name="customMessage">The message of the error. If blank will use default.</param>
        public static void IsFloatNotEqual(float value, float disallowedValue, string variableName, string customMessage = "")
        {
            if (Math.Abs(value - disallowedValue) < 0.01f) 
            {
                ThrowException(s => new PreconditionException(s), customMessage, $"{variableName} '{value.ToString()}' cannot equal {disallowedValue.ToString()}.");
            }
        }

        /// <summary>
        /// Ensures an number is bigger than a specific value.
        /// </summary>
        /// <param name="value">The number to check.</param>
        /// <param name="minSize">Minimum value allowed.</param>
        /// <param name="variableName">Name of the checked variable.</param>
        /// <param name="customMessage">The message of the error. If blank will use default.</param>
        public static void IsFloatBiggerThan(float value, float minSize, string variableName, string customMessage = "")
        {
            if (value <= minSize) 
            {
                ThrowException(s => new PreconditionException(s), customMessage, $"{variableName} '{value.ToString()}' must be above {minSize.ToString()}.");
            }
        }

        /// <summary>
        /// Ensures an number is bigger than or equal to a specific value.
        /// </summary>
        /// <param name="value">The number to check.</param>
        /// <param name="minSize">Minimum value allowed.</param>
        /// <param name="variableName">Name of the checked variable.</param>
        /// <param name="customMessage">The message of the error. If blank will use default.</param>
        public static void IsFloatBiggerOrEqualTo(float value, float minSize, string variableName, string customMessage = "")
        {
            if (value < minSize) 
            {
                ThrowException(s => new PreconditionException(s), customMessage, $"{variableName} '{value.ToString()}' must be above or equal to {minSize.ToString()}.");
            }
        }

        /// <summary>
        /// Ensures an number is lower than a specific value.
        /// </summary>
        /// <param name="value">The number to check.</param>
        /// <param name="maxSize">Maximum value allowed.</param>
        /// <param name="variableName">Name of the checked variable.</param>
        /// <param name="customMessage">The message of the error. If blank will use default.</param>
        public static void IsFloatLowerThan(float value, float maxSize, string variableName, string customMessage = "")
        {
            if (value >= maxSize) 
            {
                ThrowException(s => new PreconditionException(s), customMessage, $"{variableName} '{value.ToString()}' must be below {maxSize.ToString()}.");
            }
        }

        /// <summary>
        /// Ensures an number is lower than or equal to a specific value.
        /// </summary>
        /// <param name="value">The number to check.</param>
        /// <param name="maxSize">Maximum value allowed.</param>
        /// <param name="variableName">Name of the checked variable.</param>
        /// <param name="customMessage">The message of the error. If blank will use default.</param>
        public static void IsFloatLowerOrEqualTo(float value, float maxSize, string variableName, string customMessage = "")
        {
            if (value > maxSize) 
            {
                ThrowException(s => new PreconditionException(s), customMessage, $"{variableName} '{value.ToString()}' must be below or equal to {maxSize.ToString()}.");
            }
        }

        /// <summary>
        /// Ensures an number is within a given range (both inclusive).
        /// </summary>
        /// <param name="value">The number to check.</param>
        /// <param name="lowBounds">Minimum value allowed.</param>
        /// <param name="highBounds">Maximum value allowed.</param>
        /// <param name="variableName">Name of the checked variable.</param>
        /// <param name="customMessage">The message of the error. If blank will use default.</param>
        public static void IsFloatInRange(float value, float lowBounds, float highBounds, string variableName, string customMessage = "")
        {
            if (value < lowBounds && value > highBounds) 
            {
                ThrowException(s => new PreconditionException(s), customMessage, $"{variableName} '{value.ToString()}' must be in between {lowBounds.ToString()} - {highBounds.ToString()}.");
            }
        }
        #endregion
        
        #region String Checks
        /// <summary>
        /// Ensures a given string is not null or empty.
        /// </summary>
        /// <param name="value">The string to check.</param>
        /// <param name="variableName">Name of the variable.</param>
        /// <param name="customMessage">The message of the error. If blank will use default.</param>
        public static void IsStringNotNullOrEmpty(string value, string variableName, string customMessage = "")
        {
            if (string.IsNullOrEmpty(value))
            {
                ThrowException(s => new PreconditionException(s), customMessage, $"'{variableName}' cannot be null or empty.");
            }
        }
        
        /// <summary>
        /// Makes sure that a string is longer than minLimit.
        /// </summary>
        /// <param name="value">The string to check.</param>
        /// <param name="minLimit">Minimum characters allowed for the string.</param>
        /// <param name="variableName">Description of wronged variable.</param>
        /// <param name="customMessage">The message of the error. If blank will use default.</param>
        /// <exception cref="PreconditionException"></exception>
        public static void IsStringLengthAbove(string value, int minLimit, string variableName, string customMessage = "")
        {
            if (value.Length < minLimit)
            {
                ThrowException(s => new PreconditionException(s), customMessage, $"{variableName} '{value}' cannot have less than or equal to {minLimit.ToString()} characters.");
            }
        }

        /// <summary>
        /// Makes sure that a string is shorter than maxLimit.
        /// </summary>
        /// <param name="value">The string to check.</param>
        /// <param name="maxLimit">Maximum characters allowed for the string.</param>
        /// <param name="variableName">Description of wronged variable.</param>
        /// <param name="customMessage">The message of the error. If blank will use default.</param>
        /// <exception cref="PreconditionException"></exception>
        public static void IsStringLengthBelow(string value, int maxLimit, string variableName, string customMessage = "")
        {
            if (value.Length > maxLimit)
            {
                ThrowException(s => new PreconditionException(s), customMessage, $"{variableName} '{value}' cannot have more than or equal to {maxLimit.ToString()} characters.");
            }
        }

        /// <summary>
        /// Ensures a string's amount of characters is within a given range.
        /// </summary>
        /// <param name="value">The string to check.</param>
        /// <param name="minLimit">Range minimum.</param>
        /// <param name="maxLimit">Range maximum.</param>
        /// <param name="variableName">Name of the wronged variable.</param>
        /// <param name="customMessage">The message of the error. If blank will use default.</param>
        /// <exception cref="PreconditionException"></exception>
        public static void IsStringInRange(string value, int minLimit, int maxLimit, string variableName, string customMessage = "")
        {
            IsStringLengthAbove(value, minLimit, variableName, customMessage);
            IsStringLengthBelow(value, maxLimit, variableName, customMessage);
        }
        #endregion

        #region List Checks

        /// <summary>
        /// Ensures a given list is not empty.
        /// </summary>
        /// <param name="value">The list to check.</param>
        /// <param name="variableName">The name of the list</param>
        /// <param name="customMessage">The message of the error. If blank will use default.</param>
        /// <exception cref="PreconditionCollectionException"></exception>
        public static void IsListNotEmpty<T>(IList<T> value, string variableName, string customMessage = "")
        {
            if (value.Count <= 0) 
            {
                ThrowException(s => new PreconditionException(s), customMessage, $"{variableName} cannot be empty.");
            }
        }

        /// <summary>
        /// Ensures a given list is not null.
        /// </summary>
        /// <param name="value">The list to check.</param>
        /// <param name="variableName">The name of the list</param>
        /// <param name="customMessage">The message of the error. If blank will use default.</param>
        /// <exception cref="PreconditionCollectionException"></exception>
        public static void IsListNotNullOrEmpty<T>(IList<T> value, string variableName, string customMessage = "")
        {
            if (value == null || value.Count <= 0) 
            {
                ThrowException(s => new PreconditionException(s), customMessage, $"{variableName} cannot be empty or null.");
            }
        }

        /// <summary>
        /// Ensures a given list has a size lower than a specific size.
        /// </summary>
        /// <param name="value">The list to check.</param>
        /// <param name="maxAllowedSize">Max allowed size for the list.</param>
        /// <param name="variableName">The name of the list</param>
        /// <param name="customMessage">The message of the error. If blank will use default.</param>
        /// <exception cref="PreconditionCollectionException"></exception>
        public static void IsListIsNotLongerThan<T>(IList<T> value, int maxAllowedSize, string variableName, string customMessage = "")
        {
            if (value.Count > maxAllowedSize) 
            {
                ThrowException(s => new PreconditionException(s), customMessage, $"{variableName} length-{value.Count.ToString()} cannot have more than {maxAllowedSize.ToString()} items.");
            }
        }

        /// <summary>
        /// Ensures a given list has a size lower or equal to a specific size.
        /// </summary>
        /// <param name="value">The list to check.</param>
        /// <param name="maxAllowedSize">Max allowed size for the list.</param>
        /// <param name="variableName">The name of the list</param>
        /// <param name="customMessage">The message of the error. If blank will use default.</param>
        /// <exception cref="PreconditionCollectionException"></exception>
        public static void IsListIsNotLongerOrEqualTo<T>(IList<T> value, int maxAllowedSize, string variableName, string customMessage = "")
        {
            if (value.Count >= maxAllowedSize) 
            {
                ThrowException(s => new PreconditionException(s), customMessage, $"{variableName} length-{value.Count.ToString()} cannot have more than or equal to {maxAllowedSize.ToString()} items.");
            }
        }

        /// <summary>
        /// Ensures a given list has a size bigger than a specific value.
        /// </summary>
        /// <param name="value">The list to check.</param>
        /// <param name="minAllowedSize">Min allowed size for the list.</param>
        /// <param name="variableName">The name of the list</param>
        /// <param name="customMessage">The message of the error. If blank will use default.</param>
        /// <exception cref="PreconditionCollectionException"></exception>
        public static void IsListNotShorterThan<T>(IList<T> value, int minAllowedSize, string variableName, string customMessage = "")
        {
            if (value.Count < minAllowedSize) 
            {
                ThrowException(s => new PreconditionException(s), customMessage, $"{variableName} length-{value.Count.ToString()} cannot have less than {minAllowedSize.ToString()} items.");
            }
        }

        /// <summary>
        /// Ensures a given list has a size bigger or equal to a specific value.
        /// </summary>
        /// <param name="value">The list to check.</param>
        /// <param name="minAllowedSize">Min allowed size for the list.</param>
        /// <param name="variableName">The name of the list</param>
        /// <param name="customMessage">The message of the error. If blank will use default.</param>
        /// <exception cref="PreconditionCollectionException"></exception>
        public static void IsListNotShorterOrEqualTo<T>(IList<T> value, int minAllowedSize, string variableName, string customMessage = "")
        {
            if (value.Count <= minAllowedSize) 
            {
                ThrowException(s => new PreconditionException(s), customMessage, $"{variableName} length-{value.Count.ToString()} cannot have less than or equal to {minAllowedSize.ToString()} items.");
            }
        }

        /// <summary>
        /// Ensures a given list has a specific size.
        /// </summary>
        /// <param name="value">The list to check.</param>
        /// <param name="size">The allowed size for the list.</param>
        /// <param name="variableName">The name of the list</param>
        /// <param name="customMessage">The message of the error. If blank will use default.</param>
        /// <exception cref="PreconditionCollectionException"></exception>
        public static void IsListNotLongExactly<T>(IList<T> value, int size, string variableName, string customMessage = "")
        {
            if (value.Count == size) 
            {
                ThrowException(s => new PreconditionException(s), customMessage, $"{variableName} length-{value.Count.ToString()} must have exactly {size.ToString()} items.");
            }
        }

        /// <summary>
        /// Ensures a list does not contain a specific object.
        /// </summary>
        /// <typeparam name="T">Any object type.</typeparam>
        /// <param name="value">The list to check.</param>
        /// <param name="element">The object we check the duplicity for.</param>
        /// <param name="variableName">Name of the checked variable.</param>
        /// <param name="customMessage">The message of the error. If blank will use default.</param>
        /// <exception cref="PreconditionCollectionException"></exception>
        public static void IsListNotContaining<T>(IList<T> value, T element, string variableName, string customMessage = "")
        {
            if (value.Contains(element)) 
            {
                ThrowException(s => new PreconditionException(s), customMessage, $"{variableName} cannot contain '{element.ToString()}.'");
            }
        }
        
        public static void IsIndexWithingCollectionRange<T>(IEnumerable<T> value, int index, string collectionName, string customMessage = "")
        {
            int count = value.Count();
            if (index < 0 || index > count - 1) 
            {
                ThrowException(s => new PreconditionException(s), customMessage, $"Index ({index.ToString()}) must fit within {collectionName} of size-{count.ToString()}.");
            }
        }
        #endregion

        #region Set Checks

        public static void IsSetNotNullOrEmpty<T>(ISet<T> value, string variableName, string customMessage = "")
        {
            if (value == null || value.Count <= 0) 
            {
                ThrowException(s => new PreconditionException(s), customMessage, $"{variableName} cannot be empty or null.");
            }
        }

        #endregion
        
        #region Dictionary Checks

        public static void IsDictionaryContainingKey<TKey, TValue>(IDictionary<TKey, TValue> value, TKey key, string variableName, string customMessage = "")
        {
            if (!value.ContainsKey(key)) 
            {
                ThrowException(s => new PreconditionException(s), customMessage, $"{variableName} must contain the key '{key.ToString()}'.");
            }
        }

        #endregion

        /// <summary>
        /// Throw a custom error message, without raising an exception.
        /// </summary>
        /// <param name="message">The message to throw.</param>
        public static void ThrowMessage(string message) => OnFireErrorMessage?.Invoke(message);

        /// <summary>
        /// Throws an exception with appropriate events.
        /// </summary>
        /// <param name="exception">The type of the thrown exception</param>
        /// <param name="customMessage"></param>
        /// <param name="defaultMessage"></param>
        /// <exception cref="Exception"></exception>
        private static void ThrowException<T>(Func<string, T> exception, string customMessage, string defaultMessage) where T : Exception
        {
            string message = (customMessage != "") ? customMessage : defaultMessage;
            OnFireErrorMessage?.Invoke(message);
            throw exception(message);
        }
    }
}