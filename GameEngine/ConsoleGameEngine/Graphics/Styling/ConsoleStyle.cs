using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGameEngine.Graphics.Styling;

public readonly struct ConsoleStyle
{
    public ConsoleStyle(ConsoleColor foreground, ConsoleColor background)
    {
        this.Foreground = foreground;
        this.Background = background;
    }

    public ConsoleColor Foreground { get; init; }
    public ConsoleColor Background { get; init; }

    public void Apply()
    {
        if (Console.ForegroundColor != this.Foreground)
        {
            Console.ForegroundColor = this.Foreground;
        }

        if (Console.BackgroundColor != this.Background)
        {
            Console.BackgroundColor = this.Background;
        }
    }
}
