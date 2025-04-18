using Helpers.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers.Utility;

public class ConsoleSettings
{
    private int windowWidth;
    private int windowHeight;
    private int bufferWidth;
    private int bufferHeight;
    private ConsoleStyle style;

    public int WindowWidth
    {
        get
        {
            return this.windowWidth;
        }

        private set
        {
            Validator.NotWithin(value, 1, Console.LargestWindowWidth, nameof(this.WindowWidth));
            this.windowWidth = value;
        }
    }  

    public static ConsoleSettings Capture()
    {
        return new ConsoleSettings();
    }
}
