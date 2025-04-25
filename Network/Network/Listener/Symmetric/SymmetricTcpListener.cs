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

/// <summary>
/// Represents a symmetric TCP listener where the same message type is used for both sending and receiving.
/// Inherits from <see cref="EnhancedTcpListener{TMessage, TMessage}"/> and emits symmetric event arguments when a new client connects.
/// </summary>
/// <typeparam name="TMessage">The type of message used for both sending and receiving.</typeparam>
public class SymmetricTcpListener<TMessage> : EnhancedTcpListener<TMessage, TMessage>
    where TMessage : IMessage
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SymmetricTcpListener{TMessage}"/> class using the provided configuration.
    /// </summary>
    /// <param name="configuration">The symmetric listener configuration that includes endpoint and client setup.</param>
    public SymmetricTcpListener(SymmetricTcpListenerConfiguration<TMessage> configuration)
        : base(configuration)
    {
    }

    /// <summary>
    /// Is raised when a new symmetric client connects to the listener.
    /// </summary>
    public new event EventHandler<SymmetricTcpListenerNewClientEventArgs<TMessage>>? NewClient;

    /// <summary>
    /// Raises the <see cref="NewClient"/> event with the connected symmetric TCP client.
    /// </summary>
    /// <param name="e">The event arguments containing the connected symmetric client.</param>
    protected virtual void FireOnNewClient(SymmetricTcpListenerNewClientEventArgs<TMessage> e)
    {
        this.NewClient?.Invoke(this, e);
    }
}
