using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers.Utility.Keyboard;

public class KeyboardWatcher
{
    public bool Exit { get; set; }

    private bool HasModifier(ConsoleModifiers modifiers, ConsoleModifiers modifier)
    {
        return (modifiers & modifier) != 0;
    }

    private void WatchKeyboardWorker()
    {
        while (!this.Exit)
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey();
            KeyData keyData = new(
                keyInfo.Key, 
                this.HasModifier(keyInfo.Modifiers, ConsoleModifiers.Shift),
                this.HasModifier(keyInfo.Modifiers, ConsoleModifiers.Alt),
                this.HasModifier(keyInfo.Modifiers, ConsoleModifiers.Control));

            // sends event
        }
    }
}
