using ConsoleGameEngine.Core;
using ConsoleGameEngine.Graphics.Sprites;
using ConsoleGameEngine.Physics.Collisions;
using GameStuff.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleWyrm.Game.Entities.Wyrms;

public class WyrmSegment : GameEntity
{
    public WyrmSegment(Sprite sprite, ICollisionShape collisionShape, ConsolePosition position)
        : base(sprite, collisionShape, position)
    {
    }

    public override void TryUpdate(TimeSpan deltaTime)
    {
        // no updates from engine
    }

    public override void Update()
    {
        // For now, no logic
    }
}
