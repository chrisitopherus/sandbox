using ConsoleWyrm.Game.Entities;
using ConsoleWyrm.Game.Entities.Consumables;
using ConsoleWyrm.Game.Entities.Wyrms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleWyrm.Game.Graphics;

public class GameRenderer : IGameEntityVisitor
{
    public void Visit(GameEntity entity)
    {
        throw new NotImplementedException();
    }

    public void Visit(Wyrm wyrm)
    {
        throw new NotImplementedException();
    }

    public void Visit(Food food)
    {
        throw new NotImplementedException();
    }
}
