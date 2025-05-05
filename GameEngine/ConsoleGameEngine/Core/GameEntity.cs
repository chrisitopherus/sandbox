using ConsoleGameEngine.Graphics.Sprites;
using ConsoleGameEngine.Interfaces;
using ConsoleGameEngine.Physics.Collisions;
using ConsoleGameEngine.Physics.Collisions.Detector;
using GameStuff.Data;
using Helpers.Utility.Keyboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGameEngine.Core;

public abstract class GameEntity : IDirty, IUpdatableComponent, ICollidable
{
    protected TimeSpan updateTimer = TimeSpan.Zero;
    private ConsolePosition position;

    public GameEntity(Sprite sprite, ICollisionShape collisionShape, ConsolePosition position)
    {
        this.Sprite = sprite;
        this.CollisionShape = collisionShape;
        this.position = position;
        this.PreviousPosition = position;
    }

    public bool IsDirty
    {
        get;
        protected set;
    }

    public Scene? Scene { get; set; }

    public bool IsDespawnRequested { get; private set; }

    public TimeSpan UpdateInterval { get; protected set; }

    public ICollisionShape CollisionShape { get; protected set; }

    public Sprite Sprite { get; protected set; }

    public ConsolePosition Position
    {
        get
        {
            return this.position;
        }

        set
        {
            this.PreviousPosition = this.position;
            this.position = value;
        }
    }

    public ConsolePosition PreviousPosition
    {
        get;
        private set;
    }

    public abstract void Update();

    public void ClearDirty()
    {
        this.IsDirty = false;
    }

    public virtual void HandleKeyInput(ConsoleKeyData keyData) {}

    public virtual void TryUpdate(TimeSpan deltaTime)
    {
        this.updateTimer += deltaTime;
        if (this.updateTimer >= this.UpdateInterval)
        {
            this.Update();
            this.updateTimer = TimeSpan.Zero;
        }
    }

    public virtual void OnCollision(ICollidable other)
    {
        CollisionDispatcher.Dispatch(this, other);
    }
    public virtual void OnSpawn() { }
    public virtual void OnDespawn() { }

    protected void RequestDespawn()
    {
        this.IsDespawnRequested = true;
    }
}
