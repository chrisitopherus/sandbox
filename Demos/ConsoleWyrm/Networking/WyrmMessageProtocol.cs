using ConsoleWyrm.Networking.Messages;
using ConsoleWyrm.Networking.Messages.Codecs.Shared;
using ConsoleWyrm.Networking.Messages.Data;
using ConsoleWyrm.Networking.Messages.Shared;
using ConsoleWyrm.Utility;
using Helpers.Utility;
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

public class WyrmMessageProtocol<TMessage> : IMessageProtocol<TMessage>
    where TMessage : ICustomMessage
{
    private readonly MessageDecoderRegistry<TMessage> codecRegistry;
    private readonly int headerByteSize = 5;
    private readonly AliveMessageCodec aliveMessageCodec = new();
    private readonly AliveMessage aliveMessage = new();

    public WyrmMessageProtocol(MessageDecoderRegistry<TMessage> messageCodecRegistry)
    {
        this.codecRegistry = messageCodecRegistry;
        this.AliveMessageBytes = this.aliveMessageCodec.Encode(this.aliveMessage);
    }

    public ReadOnlyMemory<byte> AliveMessageBytes
    {
        get;
    }

    public TMessage Decode(ReadOnlyMemory<byte> data)
    {
        var span = data.Span;
        MessageType type = this.ReadMessageType(ref span);
        IMessageDecoder<TMessage> messageDecoder = this.codecRegistry.GetMessageDecoder(type);
        return messageDecoder.Decode(data);
    }

    public ReadOnlyMemory<byte> Encode(TMessage message)
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
