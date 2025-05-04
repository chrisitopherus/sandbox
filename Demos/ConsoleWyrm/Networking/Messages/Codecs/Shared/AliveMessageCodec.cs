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

public class AliveMessageCodec : MessageCodec<AliveMessage>
{
    public static AliveMessageCodec Instance = new();

    private AliveMessageCodec()
    {
    }

    public override AliveMessage Decode(ReadOnlyMemory<byte> data)
    {
        try
        {
            var span = data.Span;
            MessageType type = EnumConverter.ToMessageType(SpanReader.ReadByte(ref span));
            int contentLength = SpanReader.ReadInt(ref span);
            byte checkValue = SpanReader.ReadByte(ref span);

            if (type != MessageType.Alive || contentLength != 1 || checkValue != 69)
            {
                throw ExceptionFactory.CreateInvalidFormat(nameof(AliveMessage));
            }

            return new AliveMessage();
        }
        catch (Exception exception)
        {
            throw ExceptionFactory.CreateDecodeFailure(nameof(AliveMessage), exception);
        }
    }

    public override ReadOnlyMemory<byte> Encode(AliveMessage message)
    {
        try
        {
            int contentLength = 1;
            byte[] bytes = new byte[this.HeaderByteCount + contentLength];
            var span = bytes.AsSpan();
            SpanWriter.WriteByte(ref span, EnumConverter.ToByte(message.Type));
            SpanWriter.WriteInt(ref span, contentLength);
            SpanWriter.WriteByte(ref span, message.Check);
            return bytes;
        }
        catch (Exception exception)
        {
            throw ExceptionFactory.CreateEncodeFailure(nameof(AliveMessage), exception);
        }
    }
}
