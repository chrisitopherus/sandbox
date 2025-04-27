using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers.Utility.Lifecycle;

/// <summary>
/// Defines a component with a lifecycle.
/// </summary>
public interface ILifecycleComponent
{
    /// <summary>
    /// Is raised when the component started.
    /// </summary>
    event EventHandler? Started;

    /// <summary>
    /// Is raised when the component stopped.
    /// </summary>
    event EventHandler<LifecycleComponentStoppedEventArgs>? Stopped;

    /// <summary>
    /// Gets the current state of the component.
    /// </summary>
    LifecycleState State { get; }

    /// <summary>
    /// Starts the component.
    /// </summary>
    void Start();

    /// <summary>
    /// Stops the component.
    /// </summary>
    void Stop();
}
