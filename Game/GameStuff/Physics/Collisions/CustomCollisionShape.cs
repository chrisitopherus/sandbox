using GameStuff.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStuff.Physics.Collisions;

public class CustomCollisionShape : CollisionShape
{
    private Func<ConsolePosition[]> shapeGenerator;

    public CustomCollisionShape(ConsolePosition entityOffset, Func<ConsolePosition[]> shapeGenerator)
        : base(entityOffset)
    {
        this.shapeGenerator = shapeGenerator;
        this.RelativeCollisionCells = this.GenerateRelativeCells();
    }

    public Func<ConsolePosition[]> ShapeGenerator
    {
        get
        {
            return this.shapeGenerator;
        }

        set
        {
            this.shapeGenerator = value;
        }
    }

    public override ConsolePosition[] RelativeCollisionCells { get; }

    protected override ConsolePosition[] GenerateRelativeCells()
    {
        ConsolePosition[] result = this.shapeGenerator();
        if (result == null || result.Length == 0)
        {
            throw new InvalidOperationException("Shape generator must return at least one collision cell.");
        }

        return this.ShapeGenerator();
    }
}
