using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers.Utility;

public static class SpanReader
{
    public static byte ReadByte(ref ReadOnlySpan<byte> span)
    {
        byte value = span[0];
        span = span[1..];
        return value;
    }
}
