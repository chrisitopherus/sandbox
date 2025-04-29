using ConsoleWyrm.Game.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleWyrm.Game.Physics;

public class PointCollisionShape : RectangularCollisionShape
{
    public PointCollisionShape(ConsolePosition entityOffset)
        : base(1, 1, entityOffset)
    {
    }
}
