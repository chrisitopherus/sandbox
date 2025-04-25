using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers.Utility.Span;

public static class SpanReader
{
    public static byte ReadByte(ref ReadOnlySpan<byte> span)
    {
        byte value = span[0];
        span = span[1..];
        return value;
    }

    public static int ReadInt(ref ReadOnlySpan<byte> span)
    {
        int value = BinaryPrimitives.ReadInt32LittleEndian(span);
        span = span[4..];
        return value;
    }

    public static void Skip(ref ReadOnlySpan<byte> span, int skipAmount)
    {
        span = span[skipAmount..];
    }
}
