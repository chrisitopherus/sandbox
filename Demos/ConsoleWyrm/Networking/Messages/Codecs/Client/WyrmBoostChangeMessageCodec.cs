using ConsoleWyrm.Networking.Messages.Client;
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

namespace ConsoleWyrm.Networking.Messages.Codecs.Client;

public class WyrmBoostChangeMessageCodec : MessageCodec<WyrmBoostChangeMessage>
{
    public static readonly WyrmBoostChangeMessageCodec Instance = new();

    private WyrmBoostChangeMessageCodec() { }

    public override WyrmBoostChangeMessage Decode(ReadOnlyMemory<byte> data)
    {
        var span = data.Span;
        try
        {
            MessageType type = EnumConverter.ToMessageType(SpanReader.ReadByte(ref span));
            int contentLength = SpanReader.ReadInt(ref span);
            bool isBoostOn = SpanReader.ReadBoolean(ref span);

            if (type != MessageType.WyrmBoostChange || contentLength != 1)
            {
                throw ExceptionFactory.CreateInvalidFormat(nameof(WyrmBoostChangeMessage));
            }

            return new WyrmBoostChangeMessage(isBoostOn);
        }
        catch (Exception exception)
        {
            throw ExceptionFactory.CreateDecodeFailure(nameof(WyrmBoostChangeMessage), exception);
        }
    }

    public override ReadOnlyMemory<byte> Encode(WyrmBoostChangeMessage message)
    {
        try
        {
            int contentLength = 1;
            byte[] bytes = new byte[this.HeaderByteCount + contentLength];
            var span = bytes.AsSpan();
            SpanWriter.WriteByte(ref span, EnumConverter.ToByte(message.Type));
            SpanWriter.WriteInt(ref span, contentLength);
            SpanWriter.WriteBoolean(ref span, message.IsBoostOn);
            return bytes;
        }
        catch (Exception exception)
        {
            throw ExceptionFactory.CreateEncodeFailure(nameof(WyrmBoostChangeMessage), exception);
        }
    }
}
