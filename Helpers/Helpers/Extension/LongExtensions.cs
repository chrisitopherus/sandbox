using Helpers.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers.Extension;

public static class LongExtensions
{
    public static bool TryConvertToInt(this long value, out int result)
    {
        return IntegerParser.TryParseLongToInt(value, out result);
    }

    public static int ConvertToInt(this long value)
    {
        return (int)value;
    }
}
