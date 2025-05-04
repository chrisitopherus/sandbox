using GameStuff.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGameEngine.Physics.Collisions;

public class CircleApproxCollisionShape : CollisionShape
{
    public CircleApproxCollisionShape(ConsolePosition entityOffset)
        : base(entityOffset)
    {
        this.RelativeCollisionCells = this.GenerateRelativeCells();
    }

    public override ConsolePosition[] RelativeCollisionCells { get; }

    protected override ConsolePosition[] GenerateRelativeCells()
    {
        throw new NotImplementedException();
    }
}
