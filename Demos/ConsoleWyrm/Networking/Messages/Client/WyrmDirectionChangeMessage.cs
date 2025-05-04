using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleWyrm.Game.Data;
using ConsoleWyrm.Networking.Messages.Codecs.Client;
using ConsoleWyrm.Networking.Messages.Data;
using GameStuff.Data;

namespace ConsoleWyrm.Networking.Messages.Client;

public class WyrmDirectionChangeMessage : IClientMessage
{
    private readonly WyrmDirectionChangeMessageCodec codec = WyrmDirectionChangeMessageCodec.Instance;

    public WyrmDirectionChangeMessage(Direction newDirection)
    {
        this.NewDirection = newDirection;
    }

    public MessageType Type { get; } = MessageType.WyrmDirectionChange;

    public Direction NewDirection { get; }


    public void Accept(IClientMessageVisitor visitor)
    {
        visitor.Visit(this);
    }

    public ReadOnlyMemory<byte> Encode()
    {
        return this.codec.Encode(this);
    }
}
