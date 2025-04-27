using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers.Utility.Keyboard;

/// <summary>
/// Provides event data for a keyboard key press detected by <see cref="KeyboardWatcher"/>.
/// </summary>
public class KeyboardWatcherKeyPressedEventArgs : EventArgs
{
    /// <summary>
    /// Initializes a new instance of the <see cref="KeyboardWatcherKeyPressedEventArgs"/> class.
    /// </summary>
    /// <param name="keyData">The data associated with the pressed key.</param>
    public KeyboardWatcherKeyPressedEventArgs(ConsoleKeyData keyData)
    {
        this.KeyData = keyData;
    }

    /// <summary>
    /// Gets the data of the key that was pressed.
    /// </summary>
    public ConsoleKeyData KeyData { get; }
}
