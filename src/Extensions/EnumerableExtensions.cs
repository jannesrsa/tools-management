using System.Collections.Generic;
using System.Linq;

namespace Tools.Management.Extensions
{
    internal static class EnumerableExtensions
    {
        internal static IEnumerable<T> EmptyIfNull<T>(this IEnumerable<T> source)
        {
            return source ?? Enumerable.Empty<T>();
        }
    }
}