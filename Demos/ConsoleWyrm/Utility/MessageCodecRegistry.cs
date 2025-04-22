using ConsoleWyrm.Networking.Messages.Data;
using Network.Architecture.Interfaces.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleWyrm.Utility;

public class MessageCodecRegistry<TMessage>
{
    private readonly Dictionary<MessageType, IMessageCodec<TMessage>> codecs = [];

    public MessageCodecRegistry<TMessage> Register(MessageType type, IMessageCodec<TMessage> codec)
    {
        this.codecs.Add(type, codec);
        return this;
    }

    public IMessageCodec<TMessage> GetValue(MessageType type)
    {
        IMessageCodec<TMessage> value;
        if (!this.TryGetValue(type, out value))
        {
            throw new ArgumentOutOfRangeException($"No registered codec for type: {type}");
        }

        return value;
    }

    public bool TryGetValue(MessageType type, out IMessageCodec<TMessage> codec)
    {
        try
        {
            codec = this.codecs[type];
            return true;
        }
        catch
        {
            codec = default!;
            return false;
        }
    }
}
