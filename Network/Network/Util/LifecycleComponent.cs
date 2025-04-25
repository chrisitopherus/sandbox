using Network.Architecture;
using Network.Architecture.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network.Util;

/// <summary>
/// Represents a component with a life cycle.
/// </summary>
public abstract class LifecycleComponent : ILifecycleComponent
{
    /// <summary>
    /// The current state of the component.
    /// </summary>
    protected LifecycleState state;

    /// <summary>
    /// Gets the current state of the component.
    /// </summary>
    public LifecycleState State
    {
        get
        {
            return this.state;
        }

        protected set
        {
            if (value != this.state)
            {
                this.state = value;
                switch (this.state)
                {
                    case LifecycleState.Started:
                        this.FireOnStarted();
                        break;
                    case LifecycleState.Stopped:
                        this.FireOnStopped();
                        break;
                }
            }
        }
    }

    /// <inheritdoc />
    public event EventHandler? Started;

    /// <inheritdoc />
    public event EventHandler? Stopped;

    /// <inheritdoc />
    public abstract void Start();

    /// <inheritdoc />
    public abstract void Stop();

    /// <summary>
    /// Raises the <see cref="Started"/> event.
    /// </summary>
    protected virtual void FireOnStarted()
    {
        this.Started?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// Raises the <see cref="Stopped"/> event.
    /// </summary>
    protected virtual void FireOnStopped()
    {
        this.Stopped?.Invoke(this, EventArgs.Empty);
    }
}
