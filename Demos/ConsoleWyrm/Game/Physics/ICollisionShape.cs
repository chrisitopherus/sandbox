using ConsoleWyrm.Game.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleWyrm.Game.Physics;

public interface ICollisionShape
{
    ConsolePosition[] RelativeCollisionCells { get; }

    ConsolePosition EntityOffset { get; }

    IEnumerable<ConsolePosition> GetWorldCells(ConsolePosition entityPosition);
}
