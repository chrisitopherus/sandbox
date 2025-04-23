using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleWyrm.Networking.Messages.Data;

namespace ConsoleWyrm.Networking.Messages.Client;

public class WyrmDirectionChangeMessage : ICustomMessage<IClientMessageVisitor>
{
    public MessageType Type { get; } = MessageType.WyrmDirectionChange;

    public void Accept(IClientMessageVisitor visitor)
    {
        visitor.Visit(this);
    }

    public ReadOnlyMemory<byte> Encode()
    {
        throw new NotImplementedException();
    }
}
