using ConsoleWyrm.Networking.Messages;
using ConsoleWyrm.Networking.Messages.Data;
using Network.Architecture.Interfaces.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleWyrm.Utility;

public class MessageDecoderRegistry<TMessage> : IMessageDecoderRegistry<MessageType, TMessage>
    where TMessage : ICustomMessage
{
    private readonly Dictionary<MessageType, IMessageDecoder<TMessage>> decoders = [];

    public MessageDecoderRegistry<TMessage> Register(MessageType type, IMessageDecoder<TMessage> decoder)
    {
        this.decoders.Add(type, decoder);
        return this;
    }

    public IMessageDecoder<TMessage> GetMessageDecoder(MessageType type)
    {
        IMessageDecoder<TMessage> decoder;
        if (!this.TryGetMessageDecoder(type, out decoder))
        {
            throw new ArgumentOutOfRangeException($"No registered decoder for message type: {type}");
        }

        return decoder;
    }

    public bool TryGetMessageDecoder(MessageType type, out IMessageDecoder<TMessage> decoder)
    {
        try
        {
            decoder = this.decoders[type];
            return true;
        }
        catch
        {
            decoder = default!;
            return false;
        }
    }
}
