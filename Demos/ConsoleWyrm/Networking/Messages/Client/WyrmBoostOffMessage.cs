using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleWyrm.Networking.Messages.Codecs.Client;
using ConsoleWyrm.Networking.Messages.Data;
using Network.Architecture.Interfaces.Protocol;

namespace ConsoleWyrm.Networking.Messages.Client;

public class WyrmBoostOffMessage : Message, IClientMessage
{
    private readonly WyrmBoostOffMessageCodec codec = WyrmBoostOffMessageCodec.Instance;

    public WyrmBoostOffMessage()
        : base(MessageType.WyrmBoostOff)
    {
    }

    public void Accept(IClientMessageVisitor visitor)
    {
        visitor.Visit(this);
    }

    public override ReadOnlyMemory<byte> Encode()
    {
        return this.codec.Encode(this);
    }
}
