using Helpers.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleWyrm.Utility.Game;

public readonly struct ConsolePosition
{

    public ConsolePosition(int x = 0, int y = 0)
    {
        Validator.NotLessThan(x, 0, nameof(x));
        Validator.NotLessThan(y, 0, nameof(y));
        this.X = x;
        this.Y = y;
    }

    public int X { get; }

    public int Y { get; }

    public ConsolePosition MoveBy(int dx, int dy)
    {
        Validator.NotLessThan(this.X + dx, 0, nameof(this.X));
        Validator.NotLessThan(this.Y + dy, 0, nameof(this.Y));

        return new ConsolePosition(this.X + dx, this.Y + dy);
    }

    public ConsolePosition MoveToDirection(Direction direction) => direction switch
    {
        Direction.Up => this.MoveBy(0, -1),
        Direction.Left => this.MoveBy(-1, 0),
        Direction.Down => this.MoveBy(0, 1),
        Direction.Right => this.MoveBy(1, 0),
        _ => throw new ArgumentOutOfRangeException(nameof(direction), "Specified direction was not valid.")
    };

    public override string ToString()
    {
        return $"({this.X}|{this.Y})";
    }
}
