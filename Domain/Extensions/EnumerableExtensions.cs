using System.Collections;

namespace MyNotes.Domain.Extensions
{
    public static class EnumerableExtensions
    {
        public static bool IsNullOrEmpty<T>(this List<T> values)
        {
            return values != null && values.Any();
        }

        public static bool IsNullOrEmpty(this string val)
        {
            return string.IsNullOrEmpty(val);
        }
    }
}
