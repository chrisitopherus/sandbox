using ConsoleWyrm.Networking.Messages.Data;
using ConsoleWyrm.Networking.Messages.Shared;
using ConsoleWyrm.Utility;
using Helpers.Utility.Span;
using Network.Architecture.Interfaces.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleWyrm.Networking.Messages.Codecs.Shared;

public class AliveMessageCodec : ISymmetricMessageCodec<AliveMessage>
{
    public static AliveMessageCodec Instance = new AliveMessageCodec();

    private AliveMessageCodec()
    {
    }

    public AliveMessage Decode(ReadOnlyMemory<byte> data)
    {
        try
        {
            var span = data.Span;
            MessageType type = EnumConverter.ToMessageType(SpanReader.ReadByte(ref span));
            int contentLength = SpanReader.ReadInt(ref span);
            byte checkValue = SpanReader.ReadByte(ref span);

            if (type != MessageType.Alive || contentLength != 1 || checkValue != 69)
            {
                throw new InvalidDataException("Invalid AliveMessage format.");
            }

            return new AliveMessage();
        }
        catch (Exception exception)
        {
            throw new InvalidDataException("Failed to decode AliveMessage", exception);
        }
    }

    public ReadOnlyMemory<byte> Encode(AliveMessage message)
    {
        try
        {
            int contentLength = 1;
            byte[] bytes = new byte[message.HeaderBytesCount + contentLength];
            var span = bytes.AsSpan();
            SpanWriter.WriteByte(ref span, EnumConverter.ToByte(message.Type));
            SpanWriter.WriteInt(ref span, contentLength);
            SpanWriter.WriteByte(ref span, message.Check);
            return bytes;
        }
        catch (Exception exception)
        {
            throw new InvalidDataException("Failed to encode AliveMessage", exception);
        }
    }
}
