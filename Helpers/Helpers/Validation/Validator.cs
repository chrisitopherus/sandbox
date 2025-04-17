namespace Helpers.Validation;

public static class Validator
{
    public static void NotNull<TNullable>(TNullable value, string parameterName, string? message = null)
    {
        if (message == null)
        {
            message = $"{parameterName} must not be null.";
        }

        if (value == null)
        {
            throw new ArgumentNullException(parameterName, message);
        }
    }

    public static void NotLessThan<TComparable>(TComparable value, TComparable minValue, string parameterName, string? message = null)
        where TComparable : IComparable
    {
        if (message == null)
        {
            message = $"{parameterName} must not be less than {minValue}.";
        }

        if (value.CompareTo(minValue) < 0)
        {
            throw new ArgumentOutOfRangeException(parameterName, message);
        }
    }

    public static void NotGreaterThan<TComparable>(TComparable value, TComparable maxValue, string parameterName, string? message = null)
        where TComparable : IComparable
    {
        if (message == null)
        {
            message = $"{parameterName} must not be greater than {maxValue}.";
        }

        if (value.CompareTo(maxValue) > 0)
        {
            throw new ArgumentOutOfRangeException(parameterName, message);
        }
    }

    public static void NotWithin<TComparable>(TComparable value, TComparable minValue, TComparable maxValue, string parameterName, string? message = null)
        where TComparable : IComparable
    {
        NotLessThan(value, minValue, parameterName, message);
        NotGreaterThan(value, maxValue, parameterName, message);
    }
}
