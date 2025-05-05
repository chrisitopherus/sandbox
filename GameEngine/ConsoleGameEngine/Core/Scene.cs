using ConsoleGameEngine.Interfaces;
using Helpers.Utility.Keyboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGameEngine.Core;

public abstract class Scene : IInitializable, IRenderable
{
    public abstract IEnumerable<GameEntity> Entities { get; }

    public bool BlocksUpdate { get; protected set; } = true;

    public virtual bool BlocksRender { get; protected set; } = true;

    public virtual bool BlocksInput { get; protected set; } = true;

    public bool IsInitialized
    {
        get;
        private set;
    }

    public virtual void Update(TimeSpan deltaTime)
    {
        foreach (GameEntity entity in this.Entities)
        {
            entity.TryUpdate(deltaTime);
        }
    }

    public virtual void HandleKeyInput(ConsoleKeyData keyData)
    {
        foreach (GameEntity entity in this.Entities)
        {
            entity.HandleKeyInput(keyData);
        }
    }

    public abstract void Render();

    public void Initialize()
    {
        this.Init();
        this.IsInitialized = true;
    }

    protected abstract void Init();
}
