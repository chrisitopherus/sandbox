using Helpers.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleWyrm.Game.Data;

public readonly struct ConsolePosition
{

    public ConsolePosition(int x = 0, int y = 0)
    {
        Validator.NotLessThan(x, 0, nameof(x));
        Validator.NotLessThan(y, 0, nameof(y));
        X = x;
        Y = y;
    }

    public int X { get; }

    public int Y { get; }

    public ConsolePosition MoveBy(int dx, int dy)
    {
        Validator.NotLessThan(X + dx, 0, nameof(X));
        Validator.NotLessThan(Y + dy, 0, nameof(Y));

        return new ConsolePosition(X + dx, Y + dy);
    }

    public ConsolePosition MoveToDirection(Direction direction) => direction switch
    {
        Direction.Up => MoveBy(0, -1),
        Direction.Left => MoveBy(-1, 0),
        Direction.Down => MoveBy(0, 1),
        Direction.Right => MoveBy(1, 0),
        _ => throw new ArgumentOutOfRangeException(nameof(direction), "Specified direction was not valid.")
    };

    public override string ToString()
    {
        return $"({X}|{Y})";
    }
}
