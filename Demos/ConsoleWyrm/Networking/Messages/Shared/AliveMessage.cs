using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleWyrm.Networking.Messages.Data;

namespace ConsoleWyrm.Networking.Messages.Shared;

public class AliveMessage : IMessage<IClientMessageVisitor>, IMessage<IServerMessageVisitor>
{
    public MessageType Type => MessageType.Alive;

    public void Accept(IClientMessageVisitor visitor)
    {
        visitor.Visit(this);
    }

    public void Accept(IServerMessageVisitor visitor)
    {
        visitor.Visit(this);
    }
}
