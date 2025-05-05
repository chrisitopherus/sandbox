using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGameEngine.Interfaces;

public interface ISpawner<TSpawnable>
{
    void Spawn(TSpawnable spawnable);
}
