using ConsoleGameEngine.Graphics.Sprites;
using ConsoleGameEngine.Interfaces;
using ConsoleGameEngine.Physics.Collisions;
using GameStuff.Data;
using Helpers.Utility.Keyboard;
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

    public void ClearDirty()
    {
        this.IsDirty = false;
    }

    public virtual void HandleKeyInput(ConsoleKeyData keyData) {}

    public virtual void TryUpdate(TimeSpan deltaTime)
    {
        this.updateTimer += deltaTime;
        while (this.updateTimer >= this.UpdateInterval)
        {

            this.updateTimer -= UpdateInterval;
            Update();
        }
    }

    public abstract void Update();
}
