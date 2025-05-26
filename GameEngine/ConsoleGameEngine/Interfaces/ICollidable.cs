using ConsoleGameEngine.Physics.Collisions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGameEngine.Interfaces;

/// <summary>
/// Represents an object that can participate in collision detection.
/// </summary>
public interface ICollidable
{
    /// <summary>
    /// Gets the collision shape used for collision detection.
    /// </summary>
    ICollisionShape CollisionShape { get; }
}
