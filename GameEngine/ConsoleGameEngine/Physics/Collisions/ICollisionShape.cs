using GameStuff.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGameEngine.Physics.Collisions;

public interface ICollisionShape
{
    ConsolePosition EntityOffset { get; }

    ConsolePosition[] RelativeCollisionCells { get; }

    IEnumerable<ConsolePosition> GetWorldCells(ConsolePosition entityPosition);
}
