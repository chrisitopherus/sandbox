using ConsoleGameEngine.Graphics.Sprites;
using ConsoleGameEngine.Interfaces;
using ConsoleGameEngine.Physics.Collisions;
using GameStuff.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGameEngine.Core;

public abstract class GameEntity : IDirty, IUpdatableComponent
{
    protected TimeSpan updateTimer = TimeSpan.Zero;
    public GameEntity(Sprite sprite, ICollisionShape collisionShape, ConsolePosition position)
    {
        Sprite = sprite;
        CollisionShape = collisionShape;
        Position = position;
    }

    public bool IsDirty
    {
        get;
        protected set;
    }

    public TimeSpan UpdateInterval { get; protected set; }

    public ICollisionShape CollisionShape { get; protected set; }

    public Sprite Sprite { get; protected set; }

    public ConsolePosition Position { get; protected set; }

    public void ClearDirty()
    {
        IsDirty = false;
    }

    public virtual void TryUpdate(TimeSpan deltaTime)
    {
        updateTimer += deltaTime;
        while (updateTimer >= UpdateInterval)
        {

            updateTimer -= UpdateInterval;
            Update();
        }
    }

    public abstract void Update();
}
