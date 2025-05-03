using GameStuff.Engine.Interfaces;
using Helpers.Utility.Keyboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStuff.Engine;

public abstract class Scene
{
    public abstract void Init();

    public abstract void Update(TimeSpan deltaTime);

    public abstract void Render();

    public virtual void HandleKeyInput(ConsoleKeyData keyData) { }

    public virtual bool BlocksUpdate { get; protected set; } = true;

    public virtual bool BlocksRender { get; protected set; } = true;

    public virtual bool BlocksInput { get; protected set; } = true;
}
