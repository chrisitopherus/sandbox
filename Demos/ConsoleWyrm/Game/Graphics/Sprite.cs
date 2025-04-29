using ConsoleWyrm.Game.Interfaces;
using Helpers.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleWyrm.Game.Graphics;

public class Sprite : IUpdatable, IDirty
{
    public Sprite(string[] lines, ConsoleStyle style)
    {
        this.Lines = lines;
        this.Style = style;
    }

    public string[] Lines { get; }

    public ConsoleStyle Style { get; }

    public bool IsDirty { get; protected set; }

    public void ClearDirty()
    {
        this.IsDirty = false;
    }

    public virtual void Update(TimeSpan deltaTime)
    {
        // Static
    }
}
