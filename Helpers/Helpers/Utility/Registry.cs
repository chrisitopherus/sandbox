using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Helpers.Utility;

public static class Registry<TKey, TValue> where TKey : notnull
{
    private static readonly Dictionary<TKey, TValue> items = [];

    public static void Register(TKey key, TValue value)
    {
        items.Add(key, value);
    }

    public static TValue GetValue(TKey key)
    {
        TValue value;
        if (!TryGetValue(key, out value))
        {
            throw new ArgumentOutOfRangeException($"No item found with specified key: {key}");
        }

        return value;
    }

    public static bool TryGetValue(TKey key, out TValue value)
    {
        try
        {
            value = items[key];
            return true;
        }
        catch
        {
            value = default!;
            return false;
        }
    }
}
