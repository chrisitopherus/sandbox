using ConsoleWyrm.Networking.Messages.Codecs.Server;
using ConsoleWyrm.Networking.Messages.Data;
using Network.Architecture.Interfaces.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleWyrm.Networking.Messages.Server;

public class GameStateMessage : Message, IServerMessage
{
    private readonly GameStateMessageCodec codec = GameStateMessageCodec.Instance;

    public GameStateMessage()
        : base(MessageType.GameState)
    {
    }

    public void Accept(IServerMessageVisitor visitor)
    {
        visitor.Visit(this);
    }

    public override ReadOnlyMemory<byte> Encode()
    {
        return this.codec.Encode(this);
    }
}
