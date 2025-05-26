using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGameEngine.Interfaces;

/// <summary>
/// Represents a component that can be updated with a time delta.
/// </summary>
public interface ITickable
{
    /// <summary>
    /// Updates the component using the given time span.
    /// </summary>
    /// <param name="deltaTime">The elapsed time since the last update.</param>
    void TryUpdate(TimeSpan deltaTime);
}
