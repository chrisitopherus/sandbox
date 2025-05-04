using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGameEngine.Interfaces;

public interface ITickable
{
    void TryUpdate(TimeSpan deltaTime);
}
