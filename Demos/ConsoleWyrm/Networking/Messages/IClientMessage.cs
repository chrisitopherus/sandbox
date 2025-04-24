using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleWyrm.Networking.Messages;

public interface IClientMessage : ICustomMessage
{
    void Accept(IClientMessageVisitor visitor);
}
