using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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

    public static short ReadShort(ref ReadOnlySpan<byte> span)
    {
        short value = BinaryPrimitives.ReadInt16LittleEndian(span);
        span = span[2..];
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

    public static string ReadString(ref ReadOnlySpan<byte> span, int length)
    {
        string value = Encoding.UTF8.GetString(span[..length]);
        span = span[length..];
        return value;
    }

    public static string ReadShortLengthPrefixedString(ref ReadOnlySpan<byte> span)
    {
        short length = ReadShort(ref span);
        return ReadString(ref span, length);
    }

    public static string ReadIntLengthPrefixedString(ref ReadOnlySpan<byte> span)
    {
        int length = ReadInt(ref span);
        return ReadString(ref span, length);
    }
}
