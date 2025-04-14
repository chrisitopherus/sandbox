using Network.Architecture;
using Network.Architecture.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network.Stream;

public class EnhancedNetworkStream : ILifecycleComponent
{
    private LifecycleState state;
    public LifecycleState State
    {
        get
        {
            return this.state;
        }

        private set
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

    public void Start()
    {
        throw new NotImplementedException();
    }

    public void Stop()
    {
        throw new NotImplementedException();
    }

    protected virtual void FireOnStarted()
    {
        this.Started?.Invoke(this, EventArgs.Empty);
    }

    protected virtual void FireOnStopped()
    {
        this.Stopped?.Invoke(this, EventArgs.Empty);
    }
}
