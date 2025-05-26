using ConsoleGameEngine.Graphics.Styling;
using ConsoleGameEngine.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGameEngine.Graphics.Sprites;

public class Sprite : IDirty
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
}
