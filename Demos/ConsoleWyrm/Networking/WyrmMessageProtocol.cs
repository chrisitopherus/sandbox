using ConsoleWyrm.Networking.Messages;
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

namespace ConsoleWyrm.Networking;

public class WyrmMessageProtocol<TMessage> : IMessageProtocol<TMessage>
    where TMessage : IMessage
{
    private readonly MessageCodecRegistry<TMessage> codecRegistry;
    private readonly byte[] aliveMessageBytes = [];

    public WyrmMessageProtocol(MessageCodecRegistry<TMessage> messageCodecRegistry)
    {
        this.codecRegistry = messageCodecRegistry;
    }

    public ReadOnlyMemory<byte> AliveMessageBytes => throw new NotImplementedException();

    public TMessage Decode(ReadOnlyMemory<byte> data)
    {
        var span = data.Span;
        MessageType type = EnumConverter.ToMessageType(SpanReader.ReadByte(ref span));
        IMessageCodec<TMessage> messageCodec = this.codecRegistry.GetValue(type);
        return messageCodec.Decode(data);
    }

    public ReadOnlyMemory<byte> Encode(TMessage message)
    {
        IMessageCodec<TMessage> messageCodec = this.codecRegistry.GetValue(message.Type);
        return messageCodec.Encode(message);
    }

    public int GetMessageSize(ReadOnlyMemory<byte> buffer)
    {
        throw new NotImplementedException();
    }

    public bool IsAliveMessage(ReadOnlyMemory<byte> data)
    {
        throw new NotImplementedException();
    }
}
