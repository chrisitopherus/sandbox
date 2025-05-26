using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGameEngine.Interfaces;

/// <summary>
/// Represents a contract for removing entities from the game world.
/// </summary>
/// <typeparam name="TDespawnable">The type of object to despawn.</typeparam>
public interface IDespawner<TDespawnable>
{
    /// <summary>
    /// Despawns the specified object.
    /// </summary>
    /// <param name="despawnable">The object to despawn.</param>
    void Despawn(TDespawnable despawnable);
}

