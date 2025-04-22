using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleWyrm.Networking.Messages.Data;

namespace ConsoleWyrm.Networking.Messages;

public interface IMessage
{
    MessageType Type { get; }
}

public interface IMessage<TVisitor> : IMessage
{
    void Accept(TVisitor visitor);
}
