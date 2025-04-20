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
    private Encoding inputEncoding;
    private Encoding outputEncoding;

    public ConsoleSettings(int windowWidth, int windowHeight, int bufferWidth, int bufferHeight, ConsoleStyle style, Encoding inputEncoding, Encoding outputEncoding)
    {
        this.WindowWidth = windowWidth;
        this.WindowHeight = windowHeight;
        this.BufferWidth = bufferWidth;
        this.BufferHeight = bufferHeight;
        this.Style = style;
        this.InputEncoding = inputEncoding;
        this.OutputEncoding = outputEncoding;
    }

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

    public int WindowHeight
    {
        get
        {
            return this.windowHeight;
        }

        private set
        {
            Validator.NotWithin(value, 1, Console.LargestWindowHeight, nameof(this.WindowHeight));
            this.windowHeight = value;
        }
    }

    public int BufferWidth
    {
        get
        {
            return this.bufferWidth;
        }

        private set
        {
            Validator.NotLessThan(value, this.WindowWidth, nameof(this.BufferWidth));
            this.bufferWidth = value;
        }
    }

    public int BufferHeight
    {
        get
        {
            return this.bufferHeight;
        }

        private set
        {
            Validator.NotLessThan(value, this.WindowHeight, nameof(this.BufferHeight));
            this.bufferHeight = value;
        }
    }

    public ConsoleStyle Style
    {
        get
        {
            return this.style;
        }

        private set
        {
            Validator.NotNull(value, nameof(this.Style));
            this.style = value;
        }
    }

    public Encoding InputEncoding
    {
        get
        {
            return this.inputEncoding;
        }

        private set
        {
            Validator.NotNull(value, nameof(this.InputEncoding));
            this.inputEncoding = value;
        }
    }

    public Encoding OutputEncoding
    {
        get
        {
            return this.outputEncoding;
        }

        private set
        {
            Validator.NotNull(value, nameof(this.OutputEncoding));
            this.outputEncoding = value;
        }
    }

    public static ConsoleSettings Capture()
    {
        int windowWidth = Console.WindowWidth;
        int windowHeight = Console.WindowHeight;
        int bufferWidth = Console.BufferWidth;
        int bufferHeight = Console.BufferHeight;
        ConsoleStyle style = new ConsoleStyle(Console.ForegroundColor, Console.BackgroundColor);
        Encoding inputEncoding = Console.InputEncoding;
        Encoding outputEncoding = Console.OutputEncoding;
        return new ConsoleSettings(windowWidth, windowHeight, bufferWidth, bufferHeight, style, inputEncoding, outputEncoding);
    }
}
