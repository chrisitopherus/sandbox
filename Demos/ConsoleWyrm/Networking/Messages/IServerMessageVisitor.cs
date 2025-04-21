using ConsoleWyrm.Networking.Messages.Client;
using ConsoleWyrm.Networking.Messages.Server;
using ConsoleWyrm.Networking.Messages.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleWyrm.Networking.Messages;

public interface IServerMessageVisitor
{
    // void Visit(AliveMessage aliveMessage);

    void Visit(GameStateMessage gameStateMessage);
    void Visit(WyrmDiedMessage wyrmDiedMessage);
    void Visit(FoodSpawnedMessage foodSpawnedMessage);
    void Visit(FoodEatenMessage foodEatenMessage);
    void Visit(WyrmsUpdatedMessage wyrmsUpdatedMessage);
}
