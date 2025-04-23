using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleWyrm.Networking.Messages.Codecs.Shared;
using ConsoleWyrm.Networking.Messages.Data;
using Network.Architecture.Interfaces.Protocol;

namespace ConsoleWyrm.Networking.Messages.Shared;

public class AliveMessage : ICustomMessage
{
    private readonly AliveMessageCodec codec = new();

    public MessageType Type { get; } = MessageType.Alive;

    public byte Check { get; } = 69;

    public ReadOnlyMemory<byte> Encode()
    {
        return this.codec.Encode(this);
    }
}
