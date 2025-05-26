using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGameEngine.Interfaces;

/// <summary>
/// Defines a type that handles collisions with a specific type of collidable.
/// </summary>
/// <typeparam name="TOtherCollidable">The type of the other collidable object.</typeparam>
public interface ICollidesWith<in TOtherCollidable>
    where TOtherCollidable : ICollidable
{
    /// <summary>
    /// Called when a collision with an object of type <typeparamref name="TOtherCollidable"/> occurs.
    /// </summary>
    /// <param name="other">The other collidable object involved in the collision.</param>
    void OnCollision(TOtherCollidable other);
}
