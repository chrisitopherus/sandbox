using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers.Utility.Keyboard;

public class KeyData
{
    public KeyData(ConsoleKey key, bool shift, bool alt, bool control)
    {
        this.Key = key;
        this.Shift = shift;
        this.Alt = alt;
        this.Control = control;
    }

    public ConsoleKey Key { get; }

    public bool Shift { get; }

    public bool Alt { get; }

    public bool Control { get; }
}
