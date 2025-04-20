using ConsoleWyrm.Networking.Messages.Client;
using ConsoleWyrm.Networking.Messages.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleWyrm.Networking.Messages;

public interface IClientMessageVisitor
{
    void Visit(AliveMessage aliveMessage);
    void Visit(WyrmDirectionChangeMessage directionChangeMessage);
    void Visit(WyrmBoostOnMessage boostOnMessage);
    void Visit(WyrmBoostOffMessage boostOffMessage);
}
