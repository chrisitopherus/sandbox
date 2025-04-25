using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers.Parsing;

public static class IntegerParser
{
    public static bool TryParseLongToInt(long value, out int result)
    {
        if (value is >= int.MinValue and <= int.MaxValue)
        {
            result = (int)value;
            return true;
        }

        result = default;
        return false;
    }
}
