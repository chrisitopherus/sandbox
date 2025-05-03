using ConsoleWyrm.Game.Data;
using ConsoleWyrm.Game.Graphics;
using GameStuff.Data;
using GameStuff.Engine.Interfaces;
using GameStuff.Graphics.Sprites;
using GameStuff.Physics.Collisions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleWyrm.Game.Entities;

public abstract class GameEntity : IDirty, IUpdatableComponent
{
    protected TimeSpan updateTimer = TimeSpan.Zero;
    public GameEntity(Sprite sprite, ICollisionShape collisionShape, ConsolePosition position)
    {
        this.Sprite = sprite;
        this.CollisionShape = collisionShape;
        this.Position = position;
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

    public void Accept(IGameEntityVisitor visitor)
    {
        visitor.Visit(this);
    }

    public void ClearDirty()
    {
        this.IsDirty = false;
    }

    public virtual void TryUpdate(TimeSpan deltaTime)
    {
        this.updateTimer += deltaTime;
        while (this.updateTimer >= this.UpdateInterval)
        {

            this.updateTimer -= this.UpdateInterval;
            this.Update();
        }
    }

    public abstract void Update();
}
