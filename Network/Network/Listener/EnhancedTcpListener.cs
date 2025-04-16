using Network.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network.Listener;

public class EnhancedTcpListener<TMessage> : LifecycleComponent
{
    private readonly EnhancedTcpListenerConfiguration<TMessage> configuration;
    public EnhancedTcpListener(EnhancedTcpListenerConfiguration<TMessage> configuration)
    {
        this.configuration = configuration;
    }

    public event EventHandler<EnhancedTcpListenerNewClientEventArgs<TMessage>>? NewClient;
    public override void Start()
    {
        throw new NotImplementedException();
    }

    public override void Stop()
    {
        throw new NotImplementedException();
    }

    protected virtual void FireOnNewClient(EnhancedTcpListenerNewClientEventArgs<TMessage> e)
    {
        this.NewClient?.Invoke(this, e);
    }
}
