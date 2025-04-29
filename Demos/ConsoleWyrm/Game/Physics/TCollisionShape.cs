using ConsoleWyrm.Game.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleWyrm.Game.Physics;

public class TCollisionShape : ICollisionShape
{
    public ConsolePosition[] RelativeCollisionCells => throw new NotImplementedException();

    public ConsolePosition EntityOffset => throw new NotImplementedException();

    public IEnumerable<ConsolePosition> GetWorldCells(ConsolePosition entityPosition)
    {
        throw new NotImplementedException();
    }
}
