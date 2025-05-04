using ConsoleGameEngine.Core;
using ConsoleGameEngine.Graphics.Sprites;
using ConsoleGameEngine.Physics.Collisions;
using GameStuff.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleWyrm.Game.Entities.Consumables;

public abstract class Food : GameEntity
{
    public Food(Sprite sprite, ICollisionShape collisionShape, ConsolePosition position)
        : base(sprite, collisionShape, position)
    {
    }

    public override abstract void Update();
}
