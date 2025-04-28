using ConsoleWyrm.Networking.Messages;
using ConsoleWyrm.Networking.Messages.Codecs.Shared;
using ConsoleWyrm.Networking.Messages.Data;
using ConsoleWyrm.Networking.Messages.Shared;
using ConsoleWyrm.Utility;
using ConsoleWyrm.Utility.Messages;
using Helpers.Utility.Span;
using Microsoft.Win32;
using Network.Architecture.Interfaces.Protocol;
using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ConsoleWyrm.Networking;

public class WyrmMessageProtocol<TSendMessage, TReceiveMessage> : IMessageProtocol<TSendMessage, TReceiveMessage>
    where TSendMessage : ICustomMessage
    where TReceiveMessage : ICustomMessage
{
    private readonly MessageDecoderRegistry<TReceiveMessage> decoderRegistry;
    private readonly int headerByteSize = 5;
    private readonly AliveMessageCodec aliveMessageCodec = AliveMessageCodec.Instance;
    private readonly AliveMessage aliveMessage = new();

    public WyrmMessageProtocol(MessageDecoderRegistry<TReceiveMessage> decoderRegistry)
    {
        this.decoderRegistry = decoderRegistry;
        this.AliveMessageBytes = this.aliveMessageCodec.Encode(this.aliveMessage);
    }

    public ReadOnlyMemory<byte> AliveMessageBytes
    {
        get;
    }

    public TReceiveMessage Decode(ReadOnlyMemory<byte> data)
    {
        var span = data.Span;
        MessageType type = this.ReadMessageType(ref span);
        IMessageDecoder<TReceiveMessage> messageDecoder = this.decoderRegistry.GetMessageDecoder(type);
        return messageDecoder.Decode(data);
    }

    public ReadOnlyMemory<byte> Encode(TSendMessage message)
    {
        return message.Encode();
    }

    public int GetMessageSize(ReadOnlyMemory<byte> buffer)
    {
        var span = buffer.Span;

        // skip message type
        SpanReader.Skip(ref span, 1);
        int length = SpanReader.ReadInt(ref span);
        return length + this.headerByteSize;
    }

    public bool TryGetMessageSize(ReadOnlyMemory<byte> buffer, out int messageSize)
    {
        try
        {
            messageSize = this.GetMessageSize(buffer);
            return true;
        }
        catch
        {
            messageSize = default;
            return false;
        }
    }

    public bool IsAliveMessage(ReadOnlyMemory<byte> data)
    {
        var span = data.Span;
        MessageType type = this.ReadMessageType(ref span);
        int messageContentSize = SpanReader.ReadInt(ref span);
        if (messageContentSize != 1)
        {
            return false;
        }

        byte checkValue = SpanReader.ReadByte(ref span);
        return type == MessageType.Alive && checkValue == this.aliveMessage.Check;
    }

    private MessageType ReadMessageType(ref ReadOnlySpan<byte> span)
    {
        return EnumConverter.ToMessageType(SpanReader.ReadByte(ref span));
    }
}
