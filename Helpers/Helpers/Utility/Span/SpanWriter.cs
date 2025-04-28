using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers.Utility.Span;

public static class SpanWriter
{
    public static void WriteByte(ref Span<byte> span, byte value)
    {
        span[0] = value;
        span = span[1..];
    }

    public static void WriteShort(ref Span<byte> span, short value)
    {
        BinaryPrimitives.WriteInt16LittleEndian(span, value);
        span = span[2..];
    }

    public static void WriteInt(ref Span<byte> span, int value)
    {
        BinaryPrimitives.WriteInt32LittleEndian(span, value);
        span = span[4..];
    }

    public static void Skip(ref Span<byte> span, int skipAmount)
    {
        span = span[skipAmount..];
    }

    public static void WriteString(ref Span<byte> span, string value)
    {
        int bytesWritten = Encoding.UTF8.GetBytes(value.AsSpan(), span);
        span = span[bytesWritten..];
    }

    public static void WriteBytes(ref Span<byte> span, byte[] data)
    {
        if (data.Length > span.Length)
        {
            throw new ArgumentException("Target span too small to write data.", nameof(span));
        }

        data.CopyTo(span);
        span = span[data.Length..];
    }

    public static void WriteShortLengthPrefixedString(ref Span<byte> span, string value)
    {
        int byteCount = Encoding.UTF8.GetByteCount(value);
        if (byteCount > short.MaxValue)
        {
            throw new ArgumentException("String too long for short length prefix.", nameof(value));
        }

        WriteShort(ref span, (short)byteCount);
        WriteString(ref span, value);
    }

    public static void WriteIntLengthPrefixedString(ref Span<byte> span, string value)
    {
        int byteCount = Encoding.UTF8.GetByteCount(value);
        WriteInt(ref span, byteCount);
        WriteString(ref span, value);
    }
}
