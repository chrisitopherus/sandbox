using ConsoleGameEngine.Core;
using ConsoleGameEngine.Graphics.Sprites;
using ConsoleGameEngine.Interfaces;
using ConsoleGameEngine.Physics.Collisions;
using ConsoleWyrm.Game.Entities.Wyrms;
using GameStuff.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleWyrm.Game.Entities.Consumables;

public class Food : GameEntity, ICollidesWith<Wyrm>
{
    public Food(Sprite sprite, ICollisionShape collisionShape, ConsolePosition position)
        : base(sprite, collisionShape, position)
    {
    }

    public void OnCollision(Wyrm other)
    {
        this.RequestDespawn();
    }

    public override void TryUpdate(TimeSpan deltaTime)
    {
        // no updates
    }

    public override void Update()
    {
        // static
    }
}
