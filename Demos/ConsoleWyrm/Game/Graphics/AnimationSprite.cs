using Helpers.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleWyrm.Game.Graphics;

public class AnimationSprite : Sprite
{
    public AnimationSprite(AnimationFrame[] frames)
        : base(frames[0].Sprite.Lines, frames[0].Sprite.Style)
    {
        this.Frames = frames;
    }

    public AnimationFrame[] Frames { get; }

    public override void Update(TimeSpan deltaTime)
    {
        // Update logic
    }
}
