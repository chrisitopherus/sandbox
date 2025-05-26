using ConsoleGameEngine.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGameEngine.Graphics.Sprites;

public class AnimationSprite : Sprite, IUpdatableComponent
{
    public AnimationSprite(AnimationFrame[] frames)
        : base(frames[0].Sprite.Lines, frames[0].Sprite.Style)
    {
        this.Frames = frames;
    }

    public AnimationFrame[] Frames { get; }

    public virtual void TryUpdate(TimeSpan deltaTime)
    {
        // check if update time -> call Update
    }

    public virtual void Update()
    {
        // change sprite
        this.IsDirty = true;
    }
}
