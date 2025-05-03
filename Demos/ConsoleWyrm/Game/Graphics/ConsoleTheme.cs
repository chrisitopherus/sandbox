using Helpers.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleWyrm.Game.Graphics;

public static class ConsoleTheme
{
    public static ConsoleStyle DefaultStyle { get; } = new(ConsoleColor.White, ConsoleColor.Black);
    public static ConsoleStyle HighlightStyle { get; } = new(ConsoleColor.Black, ConsoleColor.Yellow);
}
