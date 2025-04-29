using ConsoleWyrm.Game.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleWyrm.Game.Physics;

public class RectangularCollisionShape : ICollisionShape
{
    private readonly int width;
    private readonly int height;
    public RectangularCollisionShape(int width, int height, ConsolePosition entityOffset)
    {
        this.width = width;
        this.height = height;
        this.EntityOffset = entityOffset;
        this.RelativeCollisionCells = this.GenerateRelativeCells();
    }

    public ConsolePosition EntityOffset { get; }

    public ConsolePosition[] RelativeCollisionCells { get; }

    public ConsolePosition[] GenerateRelativeCells()
    {
        ConsolePosition[] positions = new ConsolePosition[this.width * this.height];
        int index = 0; 
        for (int y = 0; y < this.height; y++)
        {
            for (int x = 0; x < this.width; x++)
            {
                positions[index++] = new ConsolePosition(x, y);
            }
        }

        return positions;
    }

    public IEnumerable<ConsolePosition> GetWorldCells(ConsolePosition entityPosition)
    {
        foreach (var cell in this.RelativeCollisionCells)
        {
            yield return new ConsolePosition(
                entityPosition.X + EntityOffset.X + cell.X,
                entityPosition.Y + EntityOffset.Y + cell.Y
            );
        }
    }
}
