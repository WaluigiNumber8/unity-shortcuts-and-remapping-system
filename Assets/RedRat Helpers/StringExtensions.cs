using System;
using System.Text;
using System.Text.RegularExpressions;

namespace RedRats.Core
{
    /// <summary>
    /// Extension methods for <see cref="string"/>.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Capitalizes the first letter.
        /// </summary>
        /// <param name="input">The string input.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">When string is empty</exception>
        /// <exception cref="ArgumentException">When string is null.</exception>
        public static string Capitalize(this string input)
        {
            return input switch
            {
                null => throw new ArgumentNullException(nameof(input)),
                "" => "",
                _ => input[0].ToString().ToUpper() + input[1..]
            };
        }
        
        /// <summary>
        /// Formats a JSON string to be more readable.
        /// </summary>
        /// <param name="json">The JSON string to format.</param>
        public static string AsPrettyJson(this string json)
        {
            StringBuilder prettyJson = new();
            bool inQuotes = false;

            for (int i = 0; i < json.Length; i++)
            {
                char ch = json[i];
                switch (ch)
                {
                    case '"':
                        prettyJson.Append(ch);
                        bool escaped = false;
                        int index = i;
                        while (index > 0 && json[--index] == '\\')
                        {
                            escaped = !escaped;
                        }

                        if (!escaped)
                        {
                            inQuotes = !inQuotes;
                        }

                        break;
                    case ':':
                        prettyJson.Append(ch);
                        if (!inQuotes)
                        {
                            prettyJson.Append(" ");
                        }

                        break;
                    default:
                        prettyJson.Append(ch);
                        break;
                }
            }
            return prettyJson.ToString();
        }

        /// <summary>
        /// Adds spaces to a Pascal Case string. Example: "PascalCase" -> "Pascal Case".
        /// </summary>
        /// <param name="s">The string to affect.</param>
        public static string WithSpacesBeforeCapitals(this string s) => Regex.Replace(s, "([A-Z])", " $1").Trim();
    }
}