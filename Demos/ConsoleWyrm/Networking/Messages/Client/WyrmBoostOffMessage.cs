using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleWyrm.Networking.Messages.Codecs.Client;
using ConsoleWyrm.Networking.Messages.Data;
using Network.Architecture.Interfaces.Protocol;

namespace ConsoleWyrm.Networking.Messages.Client;

public class WyrmBoostOffMessage : ICustomMessage<IClientMessageVisitor>
{
    private readonly WyrmBoostOffMessageCodec codec = new();

    public MessageType Type { get; } = MessageType.WyrmBoostOff;

    public void Accept(IClientMessageVisitor visitor)
    {
        visitor.Visit(this);
    }

    public ReadOnlyMemory<byte> Encode()
    {
        return this.codec.Encode(this);
    }
}
