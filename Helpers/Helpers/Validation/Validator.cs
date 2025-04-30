using System.Numerics;

namespace Helpers.Validation;

/// <summary>
/// Provides utility methods for validating method parameters and throwing descriptive exceptions when validation fails.
/// </summary>
public static class Validator
{
    /// <summary>
    /// Throws an <see cref="ArgumentNullException"/> if the specified value is <see langword="null"/>.
    /// </summary>
    /// <typeparam name="TNullable">The type of the nullable value to validate.</typeparam>
    /// <param name="value">The value to check for null.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <param name="message">An optional custom error message. If not provided, a default message is used.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="value"/> is <see langword="null"/>.</exception>
    public static void NotNull<TNullable>(TNullable value, string parameterName, string? message = null)
    {
        if (value == null)
        {
            throw new ArgumentNullException(parameterName, message ?? $"{parameterName} must not be null.");
        }
    }

    /// <summary>
    /// Throws an <see cref="ArgumentOutOfRangeException"/> if the specified value is less than the minimum allowed value.
    /// </summary>
    /// <typeparam name="TComparable">The type of the value to validate, which must implement <see cref="IComparable"/>.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="minValue">The minimum allowed value (inclusive).</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <param name="message">An optional custom error message. If not provided, a default message is used.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="value"/> is less than <paramref name="minValue"/>.</exception>
    public static void NotLessThan<TComparable>(TComparable value, TComparable minValue, string parameterName, string? message = null)
        where TComparable : IComparable
    {
        if (value.CompareTo(minValue) < 0)
        {
            throw new ArgumentOutOfRangeException(parameterName, message ?? $"{parameterName} must not be less than {minValue}.");
        }
    }

    /// <summary>
    /// Throws an <see cref="ArgumentOutOfRangeException"/> if the specified value is greater than the maximum allowed value.
    /// </summary>
    /// <typeparam name="TComparable">The type of the value to validate, which must implement <see cref="IComparable"/>.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="maxValue">The maximum allowed value (inclusive).</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <param name="message">An optional custom error message. If not provided, a default message is used.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="value"/> is greater than <paramref name="maxValue"/>.</exception>
    public static void NotGreaterThan<TComparable>(TComparable value, TComparable maxValue, string parameterName, string? message = null)
        where TComparable : IComparable
    {
        if (value.CompareTo(maxValue) > 0)
        {
            throw new ArgumentOutOfRangeException(parameterName, message ?? $"{parameterName} must not be greater than {maxValue}.");
        }
    }

    /// <summary>
    /// Throws an <see cref="ArgumentOutOfRangeException"/> if the specified value is outside the inclusive range defined by <paramref name="minValue"/> and <paramref name="maxValue"/>.
    /// </summary>
    /// <typeparam name="TComparable">The type of the value to validate, which must implement <see cref="IComparable"/>.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="minValue">The minimum allowed value (inclusive).</param>
    /// <param name="maxValue">The maximum allowed value (inclusive).</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <param name="message">An optional custom error message. If not provided, a default message is used.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown if <paramref name="value"/> is less than <paramref name="minValue"/> or greater than <paramref name="maxValue"/>.
    /// </exception>
    public static void NotWithin<TComparable>(TComparable value, TComparable minValue, TComparable maxValue, string parameterName, string? message = null)
        where TComparable : IComparable
    {
        NotLessThan(value, minValue, parameterName, message);
        NotGreaterThan(value, maxValue, parameterName, message);
    }

    public static void NotEven<TNumber>(TNumber value, string parameterName, string? message = null)
        where TNumber : INumber<TNumber>
    {
        if (value % TNumber.CreateChecked(2) == TNumber.Zero)
        {
            throw new ArgumentOutOfRangeException(parameterName, message ?? $"{parameterName} ({value}) must not be even.");
        } 
    }

    public static void NotOdd<TNumber>(TNumber value, string parameterName, string? message = null)
        where TNumber: INumber<TNumber>
    {
        if (value % TNumber.CreateChecked(2) == TNumber.One)
        {
            throw new ArgumentOutOfRangeException(parameterName, message ?? $"{parameterName} ({value}) must not be odd.");
        }
    }
}
