using GameStuff.Data;
using GameStuff.Graphics.Sprites;
using GameStuff.Physics.Collisions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleWyrm.Game.Entities.Wyrms;

public class Wyrm : GameEntity
{
    private readonly LinkedList<WyrmSegment> tail = [];
    private Direction direction = Direction.Right;
    private float size = 0;
    private TimeSpan moveTimer = TimeSpan.Zero;

    public Wyrm(Sprite sprite, ICollisionShape collisionShape, ConsolePosition position)
        : base(sprite, collisionShape, position)
    {
        this.UpdateInterval = TimeSpan.FromMilliseconds(250);
        this.IsDirty = true;
    }

    public override void Update()
    {
        this.Move();
    }

    private void Grow()
    {

    }

    private void Die()
    {

    }

    private void Move()
    {

    }
}
