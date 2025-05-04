using GameStuff.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGameEngine.Physics.Collisions;

public class PointCollisionShape : RectangularCollisionShape
{
    public PointCollisionShape()
        : base(1, 1, new ConsolePosition())
    {
    }
}
