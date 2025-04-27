using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers.Utility.Keyboard;

public class KeyboardWatcherKeyPressedEventArgs : EventArgs
{
    public KeyboardWatcherKeyPressedEventArgs(ConsoleKeyData keyData)
    {
        this.KeyData = keyData;
    }

    public ConsoleKeyData KeyData { get; }
}
