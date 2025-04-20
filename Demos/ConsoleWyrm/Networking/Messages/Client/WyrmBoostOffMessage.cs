using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleWyrm.Networking.Messages.Data;

namespace ConsoleWyrm.Networking.Messages.Client;

public class WyrmBoostOffMessage : IMessage<IClientMessageVisitor>
{
    public MessageType Type => MessageType.WyrmBoostOff;

    public void Accept(IClientMessageVisitor visitor)
    {
        visitor.Visit(this);
    }
}
