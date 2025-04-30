using GameStuff.Data;
using Helpers.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStuff.Physics.Collisions;

public class CrossCollisionShape : CollisionShape
{
    private readonly int width;
    private readonly int height;

    public CrossCollisionShape(int width, int height, ConsolePosition entityOffset)
        : base(entityOffset)
    {
        Validator.NotLessThan(width, 3, nameof(width));
        Validator.NotLessThan(height, 1, nameof(height));
        Validator.NotEven(width, nameof(width));
        this.width = width;
        this.height = height;
        this.FullHeight = height * 2 + 1;
        this.RelativeCollisionCells = GenerateRelativeCells();
    }

    public int FullHeight { get; }

    public override ConsolePosition[] RelativeCollisionCells { get; }

    protected override ConsolePosition[] GenerateRelativeCells()
    {
        ConsolePosition[] positions = new ConsolePosition[width + height * 2];

        // zero based
        int centerX = width / 2;
        int centerY = height;

        int index = 0;
        for (int y = 0; y < FullHeight; y++) // iterates each row
        {
            if (y != centerY) // not horizontal line of cross
            {
                // 1 position in the width center
                positions[index++] = new ConsolePosition(centerX, y);
            }
            else
            {
                for (int x = 0; x < width; x++) // iterates each col
                {
                    // positions for the whole row - horizontal line of cross
                    positions[index++] = new ConsolePosition(x, y);
                }
            }
        }

        return positions;
    }
}
