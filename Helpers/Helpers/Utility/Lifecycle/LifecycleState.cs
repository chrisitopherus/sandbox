using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers.Utility.Lifecycle;

/// <summary>
/// Defines the lifecycle states of a component.
/// </summary>
public enum LifecycleState
{
    /// <summary>
    /// The component has been initialized but not yet started.
    /// </summary>
    Initialized,

    /// <summary>
    /// The component is currently running.
    /// </summary>
    Started,

    /// <summary>
    /// The component has been stopped.
    /// </summary>
    Stopped
}
