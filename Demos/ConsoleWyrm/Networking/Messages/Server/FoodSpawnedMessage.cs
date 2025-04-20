using ConsoleWyrm.Networking.Messages.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleWyrm.Networking.Messages.Server;

public class FoodSpawnedMessage : IMessage<IServerMessageVisitor>
{
    public MessageType Type => MessageType.FoodSpawned;

    public void Accept(IServerMessageVisitor visitor)
    {
        visitor.Visit(this);
    }
}
