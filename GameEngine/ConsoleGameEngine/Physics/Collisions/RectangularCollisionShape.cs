using GameStuff.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGameEngine.Physics.Collisions;

public class RectangularCollisionShape : CollisionShape
{
    private readonly int width;
    private readonly int height;
    public RectangularCollisionShape(int width, int height, ConsolePosition entityOffset)
        : base(entityOffset)
    {
        this.width = width;
        this.height = height;
        this.RelativeCollisionCells = GenerateRelativeCells();
    }

    public override ConsolePosition[] RelativeCollisionCells { get; }

    protected override ConsolePosition[] GenerateRelativeCells()
    {
        ConsolePosition[] positions = new ConsolePosition[width * height];
        int index = 0;
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                positions[index++] = new ConsolePosition(x, y);
            }
        }

        return positions;
    }
}
