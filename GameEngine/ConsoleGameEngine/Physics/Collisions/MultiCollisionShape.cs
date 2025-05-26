using GameStuff.Data;
using Helpers.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGameEngine.Physics.Collisions;

public class MultiCollisionShape : CollisionShape
{
    public MultiCollisionShape(IEnumerable<ICollisionShape> collisionShapes, ConsolePosition entityOffset)
        : base(entityOffset)
    {
        Validator.NotNull(collisionShapes, nameof(collisionShapes));
        this.CollisionShapes = collisionShapes;
        this.RelativeCollisionCells = this.GenerateRelativeCells();
    }

    public IEnumerable<ICollisionShape> CollisionShapes { get; }

    public override ConsolePosition[] RelativeCollisionCells { get; }

    protected override ConsolePosition[] GenerateRelativeCells()
    {
        HashSet<ConsolePosition> cells = [];
        foreach (ICollisionShape collisionShape in this.CollisionShapes)
        {
            foreach (ConsolePosition cellPosition in collisionShape.RelativeCollisionCells)
            {
                cells.Add(cellPosition);
            }
        }

        return [..cells];
    }
}
