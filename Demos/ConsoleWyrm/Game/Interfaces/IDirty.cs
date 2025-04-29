using ConsoleWyrm.Game.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleWyrm.Game.Interfaces;

public interface IDirty
{
    bool IsDirty { get; }

    void ClearDirty();
}
