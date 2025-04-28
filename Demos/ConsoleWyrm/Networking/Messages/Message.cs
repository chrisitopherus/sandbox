using ConsoleWyrm.Networking.Messages.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleWyrm.Networking.Messages;

public abstract class Message : ICustomMessage
{   
    public Message(MessageType messageType)
    {
        this.Type = messageType;
    }

    public byte HeaderBytesCount { get; } = 5;

    public MessageType Type { get; }

    public abstract ReadOnlyMemory<byte> Encode();
}
