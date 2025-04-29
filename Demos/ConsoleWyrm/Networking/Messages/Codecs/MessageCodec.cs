using Network.Architecture.Interfaces.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleWyrm.Networking.Messages.Codecs;

public abstract class MessageCodec<TMessage> : ISymmetricMessageCodec<TMessage>
    where TMessage : ICustomMessage
{
    public byte HeaderByteCount { get; } = 5;

    public abstract TMessage Decode(ReadOnlyMemory<byte> data);

    public abstract ReadOnlyMemory<byte> Encode(TMessage message);
}
