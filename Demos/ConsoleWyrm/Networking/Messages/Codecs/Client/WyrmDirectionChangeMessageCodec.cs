using ConsoleWyrm.Game.Data;
using ConsoleWyrm.Networking.Messages.Client;
using ConsoleWyrm.Networking.Messages.Data;
using ConsoleWyrm.Utility;
using Helpers.Utility.Span;
using Network.Architecture.Interfaces.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleWyrm.Networking.Messages.Codecs.Client;

public class WyrmDirectionChangeMessageCodec : MessageCodec<WyrmDirectionChangeMessage>
{
    public static readonly WyrmDirectionChangeMessageCodec Instance = new();

    private WyrmDirectionChangeMessageCodec() { }

    public override WyrmDirectionChangeMessage Decode(ReadOnlyMemory<byte> data)
    {
        try
        {
            var span = data.Span;
            MessageType type = EnumConverter.ToMessageType(SpanReader.ReadByte(ref span));
            int contentLength = SpanReader.ReadInt(ref span);
            Direction direction = EnumConverter.ToDirection(SpanReader.ReadByte(ref span));

            if (type != MessageType.WyrmDirectionChange || contentLength != 1)
            {
                throw ExceptionFactory.CreateInvalidFormat(nameof(WyrmDirectionChangeMessage));
            }

            return new WyrmDirectionChangeMessage(direction);
        }
        catch (Exception exception)
        {
            throw ExceptionFactory.CreateDecodeFailure(nameof(WyrmDirectionChangeMessage), exception);
        }
    }

    public override ReadOnlyMemory<byte> Encode(WyrmDirectionChangeMessage message)
    {
        try
        {
            int contentLength = 1;
            byte[] bytes = new byte[this.HeaderByteCount + contentLength];
            var span = bytes.AsSpan();
            SpanWriter.WriteByte(ref span, EnumConverter.ToByte(message.Type));
            SpanWriter.WriteInt(ref span, contentLength);
            SpanWriter.WriteByte(ref span, EnumConverter.ToByte(message.NewDirection));
            return bytes;
        }
        catch (Exception exception)
        {
            throw ExceptionFactory.CreateEncodeFailure(nameof(WyrmBoostChangeMessage), exception);
        }
    }
}
