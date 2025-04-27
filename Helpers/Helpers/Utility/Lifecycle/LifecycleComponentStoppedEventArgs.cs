using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers.Utility.Lifecycle;

public class LifecycleComponentStoppedEventArgs : EventArgs
{
    public LifecycleComponentStoppedEventArgs()
    {
    }

    public LifecycleComponentStoppedEventArgs(Exception? exception)
    {
        this.Exception = exception;
    }

    public Exception? Exception { get; }
}
