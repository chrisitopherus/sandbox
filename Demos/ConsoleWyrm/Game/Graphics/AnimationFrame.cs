using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleWyrm.Game.Graphics;

public class AnimationFrame
{
    public AnimationFrame(Sprite sprite, TimeSpan duration)
    {
        this.Sprite = sprite;
        this.Duration = duration;
    }

    public Sprite Sprite { get;}
    public TimeSpan Duration { get; }
}
