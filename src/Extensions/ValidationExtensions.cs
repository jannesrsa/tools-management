using System;
using SourceCode.Tools.Management.Properties;

namespace SourceCode.Tools.Management.Extensions
{
    public static class ValidationExtensions
    {
        /// <summary>
        /// Checks if the object is null
        /// </summary>
        /// <typeparam name="T">Type of the object being checked</typeparam>
        /// <param name="value"></param>
        /// <param name="name"></param>
        public static void ThrowIfNull<T>(this T value, string name) where T : class
        {
            if (value == null)
                throw new ArgumentException(Resources.ErrorRequiredEmpty, name);
        }

        /// <summary>
        /// Checks if a string is null or whitespace
        /// </summary>
        /// <param name="value"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static void ThrowIfNullOrWhiteSpace(this string value, string name)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException(Resources.ErrorRequiredEmpty, name);
        }
    }
}