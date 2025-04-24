using Network.Architecture;
using Network.Architecture.Interfaces;
using Network.Client;
using Network.Listener.Configuration;
using Network.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Network.Listener.Symmetric;

public class SymmetricTcpListener<TMessage> : EnhancedTcpListener<TMessage, TMessage>
    where TMessage : IMessage
{
    public SymmetricTcpListener(SymmetricTcpListenerConfiguration<TMessage> configuration)
        : base(configuration)
    {
    }

    public new event EventHandler<SymmetricTcpListenerNewClientEventArgs<TMessage>>? NewClient;

    protected virtual void FireOnNewClient(SymmetricTcpListenerNewClientEventArgs<TMessage> e)
    {
        this.NewClient?.Invoke(this, e);
    }
}
