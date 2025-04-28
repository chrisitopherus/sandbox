using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleWyrm.Networking.Messages.Codecs.Client;
using ConsoleWyrm.Networking.Messages.Data;
using Network.Architecture.Interfaces.Protocol;

namespace ConsoleWyrm.Networking.Messages.Client;

public class WyrmBoostChangeMessage : Message, IClientMessage
{
    private readonly WyrmBoostChangeMessageCodec codec = WyrmBoostChangeMessageCodec.Instance;

    public WyrmBoostChangeMessage(bool isBoostOn)
        : base(MessageType.WyrmBoostChange)
    {
        this.IsBoostOn = isBoostOn;
    }

    public bool IsBoostOn { get; }

    public void Accept(IClientMessageVisitor visitor)
    {
        visitor.Visit(this);
    }

    public override ReadOnlyMemory<byte> Encode()
    {
        return this.codec.Encode(this);
    }
}
