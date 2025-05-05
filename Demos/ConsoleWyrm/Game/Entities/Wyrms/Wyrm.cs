using ConsoleGameEngine.Core;
using ConsoleGameEngine.Graphics.Sprites;
using ConsoleGameEngine.Physics.Collisions;
using ConsoleWyrm.Game.Ressources;
using GameStuff.Data;
using Helpers.Utility.Keyboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleWyrm.Game.Entities.Wyrms;

public class Wyrm : GameEntity
{
    private readonly LinkedList<WyrmSegment> tail = [];
    private float size = 0;
    private TimeSpan moveTimer = TimeSpan.Zero;
    private Sprite headSprite = RessourceRegistry.GetSprite(SpriteId.WyrmHead);
    private Sprite tailSprite = RessourceRegistry.GetSprite(SpriteId.WyrmTail);

    public Wyrm(Sprite sprite, ICollisionShape collisionShape, ConsolePosition position)
        : base(sprite, collisionShape, position)
    {
        this.UpdateInterval = TimeSpan.FromMilliseconds(50);
        this.CurrentDirection = Direction.Right;
        this.IsDirty = true;
    }

    public Direction CurrentDirection { get; private set; }

    public LinkedList<WyrmSegment> Tail
    {
        get
        {
            return this.tail;
        }
    }

    public override void Update()
    {
        this.Move();
    }

    public override void HandleKeyInput(ConsoleKeyData keyData)
    {
        if (this.TryKeyToDirection(keyData.Key, out Direction direction))
        {
            this.CurrentDirection = direction;
        }

        if (keyData.Control && keyData.Key == ConsoleKey.G)
        {
            this.Grow();
            this.IsDirty = true;
        }
    }

    private void Grow()
    {
        ICollisionShape collisionShape = RessourceRegistry.GetCollisionShape(CollisionShapeId.Point);
        WyrmSegment? lastSegment = this.tail.Last?.Value;
        ConsolePosition refPos = lastSegment?.Position ?? this.Position;
        ConsolePosition position = refPos.Add(new ConsolePosition(1, 0));
        WyrmSegment wyrmSegment = new(this.tailSprite, collisionShape, position);
        this.tail.AddLast(wyrmSegment);
    }

    private void Die()
    {

    }

    private void Move()
    {
        ConsolePosition prevPos = this.Position;
        this.Position = this.Position.MoveToDirection(this.CurrentDirection);
        foreach (WyrmSegment wyrmSegment in this.tail)
        {
            (prevPos, wyrmSegment.Position) = (wyrmSegment.Position, prevPos);
        }

        this.IsDirty = true;
    }

    private bool TryKeyToDirection(ConsoleKey key, out Direction direction)
    {
        try
        {
            direction = this.KeyToDirection(key);
            return true;
        }
        catch
        {
            direction = default;
            return false;
        }
    }

    private Direction KeyToDirection(ConsoleKey key) => key switch
    {
        ConsoleKey.UpArrow => Direction.Up,
        ConsoleKey.DownArrow => Direction.Down,
        ConsoleKey.LeftArrow => Direction.Left,
        ConsoleKey.RightArrow => Direction.Right,
        _ => throw new ArgumentOutOfRangeException(nameof(key), "The specfied key is not bind to a direction."),
    };
}
