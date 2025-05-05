using Helpers.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStuff.Data;

public readonly struct ConsolePosition
{

    public ConsolePosition(int x = 0, int y = 0)
    {
        this.X = x;
        this.Y = y;
    }

    public int X { get; }

    public int Y { get; }

    public ConsolePosition MoveBy(int dx, int dy)
    {
        return new ConsolePosition(this.X + dx, this.Y + dy);
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
        return $"({this.X}|{this.Y})";
    }

    public ConsolePosition Add(ConsolePosition position)
    {
        return new ConsolePosition(this.X + position.X, this.Y + position.Y);
    }

    public bool Equal(ConsolePosition position)
    {
        return this.X == position.X && this.Y == position.Y;
    }

    public ConsolePosition Negate()
    {
        return new ConsolePosition(-this.X, -this.Y);
    }
}