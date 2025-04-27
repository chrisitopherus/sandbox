using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers.Utility.Keyboard;

public readonly struct ConsoleKeyData
{
    public ConsoleKeyData(ConsoleKey key, bool shift, bool alt, bool control)
    {
        this.Key = key;
        this.Shift = shift;
        this.Alt = alt;
        this.Control = control;
    }

    public ConsoleKey Key { get; init; }

    public bool Shift { get; init; }

    public bool Alt { get; init; }

    public bool Control { get; init; }
}
