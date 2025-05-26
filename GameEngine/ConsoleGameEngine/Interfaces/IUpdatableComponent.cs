using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGameEngine.Interfaces;

/// <summary>
/// Represents a component that supports both logic update and timed updates.
/// </summary>
public interface IUpdatableComponent : IUpdatable, ITickable
{
}
