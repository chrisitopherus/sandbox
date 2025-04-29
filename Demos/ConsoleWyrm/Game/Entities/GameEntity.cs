using ConsoleWyrm.Game.Data;
using ConsoleWyrm.Game.Graphics;
using ConsoleWyrm.Game.Interfaces;
using ConsoleWyrm.Game.Physics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleWyrm.Game.Entities;

public abstract class GameEntity : IDirty
{
    public GameEntity(ICollisionShape collisionShape, Sprite sprite, ConsolePosition position)
    {
        this.CollisionShape = collisionShape;
        this.Sprite = sprite;
        this.Position = position;
    }

    public bool IsDirty
    {
        get;
        private set;
    }

    public ICollisionShape CollisionShape { get; }

    public Sprite Sprite { get; }

    public ConsolePosition Position { get; }

    public void ClearDirty()
    {
        this.IsDirty = false;
    }
}
