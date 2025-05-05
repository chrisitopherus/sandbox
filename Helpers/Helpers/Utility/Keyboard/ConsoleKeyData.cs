using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers.Utility.Keyboard;

/// <summary>
/// Immutable data structure that holds information about a console key press, including modifier states.
/// </summary>
public readonly struct ConsoleKeyData
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ConsoleKeyData"/> struct.
    /// </summary>
    /// <param name="key">The primary key that was pressed.</param>
    /// <param name="shift">Whether the Shift key was pressed.</param>
    /// <param name="alt">Whether the Alt key was pressed.</param>
    /// <param name="control">Whether the Control key was pressed.</param>
    public ConsoleKeyData(ConsoleKey key, bool shift, bool alt, bool control)
    {
        this.Key = key;
        this.Shift = shift;
        this.Alt = alt;
        this.Control = control;
    }

    /// <summary>
    /// Gets the primary key that was pressed.
    /// </summary>
    public ConsoleKey Key { get; init; }

    /// <summary>
    /// Gets a value indicating whether the Shift key was pressed.
    /// </summary>
    public bool Shift { get; init; }

    /// <summary>
    /// Gets a value indicating whether the Alt key was pressed.
    /// </summary>
    public bool Alt { get; init; }

    /// <summary>
    /// Gets a value indicating whether the Control key was pressed.
    /// </summary>
    public bool Control { get; init; }

    public override string ToString()
    {
        string shift = this.Shift ? "Shift + " : string.Empty;
        string alt = this.Alt ? "Alt + " : string.Empty;
        string ctrl = this.Control ? "Ctrl + " : string.Empty;

        return $"{ctrl}{alt}{shift}{this.Key}";
    }
}
