using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleWyrm.Networking.Messages.Data;

namespace ConsoleWyrm.Networking.Messages;

public interface IMessage<TVisitor>
{
    MessageType Type { get; }
    void Accept(TVisitor visitor);
}
