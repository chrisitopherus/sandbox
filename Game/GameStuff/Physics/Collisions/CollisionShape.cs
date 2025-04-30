using GameStuff.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStuff.Physics.Collisions;

public abstract class CollisionShape : ICollisionShape
{
    public CollisionShape(ConsolePosition entityOffset)
    {
        this.EntityOffset = entityOffset;
    }

    public ConsolePosition EntityOffset { get; }

    public abstract ConsolePosition[] RelativeCollisionCells { get; }

    public IEnumerable<ConsolePosition> GetWorldCells(ConsolePosition entityPosition)
    {
        int xOffset = entityPosition.X + this.EntityOffset.X;
        int yOffset = entityPosition.Y + this.EntityOffset.Y;

        foreach (var cell in RelativeCollisionCells)
        {
            yield return new ConsolePosition(
                cell.X + xOffset,
                cell.Y + yOffset
            );
        }
    }

    protected abstract ConsolePosition[] GenerateRelativeCells();
}
