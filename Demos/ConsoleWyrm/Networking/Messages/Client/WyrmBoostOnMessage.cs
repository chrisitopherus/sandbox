using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleWyrm.Networking.Messages.Data;

namespace ConsoleWyrm.Networking.Messages.Client;

public class WyrmBoostOnMessage : IClientMessage
{
    public MessageType Type { get; } = MessageType.WyrmBoostOn;

    public void Accept(IClientMessageVisitor visitor)
    {
        visitor.Visit(this);
    }

    public ReadOnlyMemory<byte> Encode()
    {
        throw new NotImplementedException();
    }
}
