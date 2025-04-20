using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleWyrm.Networking.Messages.Data;

namespace ConsoleWyrm.Networking.Messages.Server;

public class WyrmDiedMessage : IMessage<IServerMessageVisitor>
{
    public MessageType Type => MessageType.WyrmDied;

    public void Accept(IServerMessageVisitor visitor)
    {
        visitor.Visit(this);
    }
}
