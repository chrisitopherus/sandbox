using GameStuff.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStuff.Physics.Collisions;

public class PointCollisionShape : RectangularCollisionShape
{
    public PointCollisionShape()
        : base(1, 1, new ConsolePosition())
    {
    }
}
