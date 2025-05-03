using ConsoleWyrm.Game.Entities;
using ConsoleWyrm.Game.Entities.Consumables;
using ConsoleWyrm.Game.Entities.Wyrms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleWyrm.Game.Graphics;

public interface IGameEntityVisitor
{
    void Visit(GameEntity entity);

    void Visit(Wyrm wyrm);

    void Visit(Food food);
}
