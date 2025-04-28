using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleWyrm.Networking.Messages.Codecs.Client;
using ConsoleWyrm.Networking.Messages.Data;

namespace ConsoleWyrm.Networking.Messages.Client;

public class WyrmDirectionChangeMessage : Message, IClientMessage
{
    private readonly WyrmDirectionChangeMessageCodec codec = WyrmDirectionChangeMessageCodec.Instance;

    public WyrmDirectionChangeMessage()
        : base(MessageType.WyrmDirectionChange)
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
