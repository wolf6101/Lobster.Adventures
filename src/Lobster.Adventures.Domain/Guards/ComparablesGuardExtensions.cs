namespace Ardalis.GuardClauses
{
    public static class ComparablesGuardExtensions
    {
        /// <summary>
        /// Throws ArgumentOutOfRangeException if value greater than other value
        /// </summary>
        /// <typeparam name="TValue">IComparable not nullable type</typeparam>
        /// <param name="value">Value to be checked against other value</param>
        /// <param name="other">Other value to be compared with</param>
        /// <param name="paramName">Name of the parameter</param>
        /// <param name="message">Custom Exception message</param>
        /// <exception cref="InvalidOperationException"></exception>
        public static void GreaterThan<TValue>(this IGuardClause guardClause, TValue value, TValue other, string? message = "")
            where TValue : notnull, IComparable
        {
            if (Comparer<TValue>.Default.Compare(value, other) > 0)
            {
                if (!string.IsNullOrEmpty(message))
                {
                    throw new InvalidOperationException(message);
                }

                throw new InvalidOperationException($"Value should not be greater than {other}, but it is {value}");
            }
        }
    }
}