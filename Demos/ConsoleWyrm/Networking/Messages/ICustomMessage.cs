using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleWyrm.Networking.Messages.Data;
using Network.Architecture.Interfaces;

namespace ConsoleWyrm.Networking.Messages;

public interface ICustomMessage : IMessage
{
    MessageType Type { get; }
}

public interface ICustomMessage<TVisitor> : ICustomMessage
{
    void Accept(TVisitor visitor);
}
