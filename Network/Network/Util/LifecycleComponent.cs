using Network.Architecture;
using Network.Architecture.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network.Util;

public abstract class LifecycleComponent : ILifecycleComponent
{
    protected LifecycleState state;
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

    public event EventHandler? Started;
    public event EventHandler? Stopped;

    public abstract void Start();

    public abstract void Stop();

    protected virtual void FireOnStarted()
    {
        this.Started?.Invoke(this, EventArgs.Empty);
    }

    protected virtual void FireOnStopped()
    {
        this.Stopped?.Invoke(this, EventArgs.Empty);
    }
}
