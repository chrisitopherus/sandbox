using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGameEngine.Interfaces;

/// <summary>
/// Represents an interface for adding entities to the game world.
/// </summary>
/// <typeparam name="TSpawnable">The type of object to spawn.</typeparam>
public interface ISpawner<TSpawnable>
{
    /// <summary>
    /// Spawns the specified object.
    /// </summary>
    /// <param name="spawnable">The object to spawn.</param>
    void Spawn(TSpawnable spawnable);
}
