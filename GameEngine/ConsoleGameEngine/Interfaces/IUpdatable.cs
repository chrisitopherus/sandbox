using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGameEngine.Interfaces;

/// <summary>
/// Represents a component with a logic update step.
/// </summary>
public interface IUpdatable
{
    /// <summary>
    /// Performs a logic update.
    /// </summary>
    void Update();
}
