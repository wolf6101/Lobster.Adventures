using System;

using Ardalis.GuardClauses;

using Xunit;

namespace Lobster.Adventures.UnitTests.Domain
{
    public class ComparablesGuardExtensionsTest
    {
        [Theory]
        [InlineData(5, 0)]
        [InlineData(-25, -100)]
        [InlineData(-1.5, -1.6)]
        [InlineData(-1.99999, -2)]
        [InlineData("b", "a")]
        public void GreaterThanGuard_ValueIsGreaterThanOther_ShouldThrowException<T>(T value, T other) where T : notnull, IComparable
        {
            // Assert
            Assert.Throws<InvalidOperationException>(() => Guard.Against.GreaterThan<T>(value, other));
        }

        [Theory]
        [InlineData(1, 20)]
        [InlineData(-25, -24)]
        [InlineData(0, 0)]
        [InlineData(-1.99999, -1.99998)]
        [InlineData("a", "b")]
        public void GreaterThanGuard_ValueIsLesserThanOther_ShouldPass<T>(T value, T other) where T : notnull, IComparable
        {
            // Assert
            var exception = Record.Exception(() => Guard.Against.GreaterThan<T>(value, other));

            // Assert
            Assert.Null(exception);
        }
    }
}